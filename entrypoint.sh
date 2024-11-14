#!/bin/bash

# Apply database migrations
echo "Applying database migrations..."
dotnet ef database update --project /app/Wishlist.csproj

# Run the application
echo "Starting the application..."
exec dotnet Wishlist.dll