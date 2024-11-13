# Stage 1: Base Image - A runtime environment to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Ensure the app runs with a non-root user (optional but recommended for security)
# If you don't need to use $APP_UID, you can remove the USER line or define it explicitly
# Example: RUN adduser --disabled-password --gecos "" myappuser && chown -R myappuser /app
USER appuser  # If you have a user, otherwise leave this line out or use 'root'

WORKDIR /app
EXPOSE 8080  # Ensure this matches your app's listening port
EXPOSE 8081  # If needed, ensure your app listens on this port as well

# Stage 2: Build Image - Build your app using the .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src
COPY ["Wishlist.csproj", "./"]
RUN dotnet restore "Wishlist.csproj"  # Restore dependencies

COPY . .  # Copy the entire source code to the build context
WORKDIR "/src/"
RUN dotnet build "Wishlist.csproj" -c $BUILD_CONFIGURATION -o /app/build  # Build the app

# Stage 3: Publish Image - Publish the app for production
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Wishlist.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false  # Publish the app

# Stage 4: Final Image - Use the base image and copy the published files from the previous stage
FROM base AS final
WORKDIR /app

# Copy the published app from the publish stage
COPY --from=publish /app/publish .

# Set the entry point to run the app
ENTRYPOINT ["dotnet", "Wishlist.dll"]

# Note: Ensure your app listens on the ports exposed (8080, 8081)
