Repro steps:
1. Clone this repository
2. Open solution, set RangeDemo as startup project (ignore failure to load Swashbuckle projects)
3. Run the project, SwaggerUI page opens
4. Expand ExampleModel > Fraction
5. Observe `maximum` and `minimum` are missing
6. Clone https://github.com/bkoelman/Swashbuckle.AspNetCore in sibling directory, checkout branch `fix-range-typeconverter`
7. Edit `RangeDemo.csproj`, uncomment the `BuildSwashbuckleFromSource` line
8. Reload unloaded projects, rebuild the solution (multiple times if needed)
9. Run the project, SwaggerUI page opens
10. Expand ExampleModel > Fraction
11. Observe `maximum` and `minimum` are present
