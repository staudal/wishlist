import { json, LoaderFunctionArgs, redirect } from '@remix-run/router'
import { Outlet, useLoaderData, useLocation } from '@remix-run/react'
import { AppSidebar } from '~/components/app-sidebar.tsx'
import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbPage,
  BreadcrumbSeparator,
} from '~/components/ui/breadcrumb.tsx'
import { Separator } from '~/components/ui/separator.tsx'
import {
  SidebarInset,
  SidebarProvider,
  SidebarTrigger,
} from '~/components/ui/sidebar.tsx'
import { authenticator } from '~/modules/auth/auth.server.ts'
import { prisma } from '~/utils/db.server.ts'

export async function loader({ request }: LoaderFunctionArgs) {
  const session = await authenticator.isAuthenticated(request, {
    failureRedirect: '/login',
  })

  const user = await prisma.user.findUnique({ where: { id: session.id } })
  if (!user) return redirect('/login')

  return json({ user } as const)
}

export default function Page() {
  const { user } = useLoaderData<typeof loader>()
  const location = useLocation()
  const pathSegments = location.pathname.split('/').filter(Boolean)

  const capitalize = (str: string) => str.charAt(0).toUpperCase() + str.slice(1)

  return (
    <SidebarProvider>
      <AppSidebar user={user} />
      <SidebarInset>
        <header className="flex h-16 shrink-0 items-center gap-2 border-b transition-[width,height] ease-linear group-has-[[data-collapsible=icon]]/sidebar-wrapper:h-12">
          <div className="flex items-center gap-2 px-4">
            <SidebarTrigger className="-ml-1" />
            <Separator orientation="vertical" className="mr-2 h-4" />
            <Breadcrumb>
              <BreadcrumbList>
                {pathSegments.map((segment, index) => {
                  const path = `/${pathSegments.slice(0, index + 1).join('/')}`
                  const isLast = index === pathSegments.length - 1
                  const formattedSegment = capitalize(decodeURIComponent(segment))

                  return (
                    <BreadcrumbItem key={path}>
                      {isLast ? (
                        <BreadcrumbPage>{formattedSegment}</BreadcrumbPage>
                      ) : (
                        <>
                          <BreadcrumbLink href={path}>{formattedSegment}</BreadcrumbLink>
                          <BreadcrumbSeparator />
                        </>
                      )}
                    </BreadcrumbItem>
                  )
                })}
              </BreadcrumbList>
            </Breadcrumb>
          </div>
        </header>
        <div className="flex flex-1 flex-col gap-4 p-4">
          <Outlet />
        </div>
      </SidebarInset>
    </SidebarProvider>
  )
}
