dotnet pack -c Release -o bin/Release -p:PackageVersion=1.0.4 --include-symbols
dotnet nuget push "bin/Release/Ascetic.Core.Http.1.0.4.nupkg" --skip-duplicate --api-key oy2ce65tow5ua6eemedqxrd2r7hjszruhfqu3sskmqgdba --source https://api.nuget.org/v3/index.json
