# AssemblyAttribute

Reusable base attribute designed to streamline the process of using auto-versioning scripts in C# project files.  

## Usage

The `BaseAssemblyAttribute` abstract class accepts any number of strings via `params string[]`, but since it is an abstract class it cannot be used directly.  
You can use it to create your own assembly attributes, or just as example code - the implementation is very simple.  

### `ExtendedVersionAttribute`

Derives from `BaseAssemblyAttribute` and acts as a method to retrieve the **full version string** and **an array of its segments** after being split by all occurrences of `.`, `-`, or `+` via the `ExtendedVersionAttribute.Version` and `ExtendedVersionAttribute.VersionSegments` properties, respectively.  
I use it with my auto-versioning scripts so I can retrieve the latest git tag in the local repository directly in the code, and that's what this guide will focus on.

 1.	Add the **[AssemblyAttribute](https://www.nuget.org/packages/AssemblyAttribute nuget package)** to your project.
 2.	Open the `.csproj` project file in the editor.  
    *For .NET Core projects, you can do this by Right-Clicking on the project in the solution explorer & selecting the **Edit Project File** option.*
 3. Add an element within the `PropertyGroup` tag to store the version number:  
    ```csproj
    <ExtendedVersion></ExtendedVersion>
    ```
 4. Add a new `ItemGroup` element within the `Project` tag to add the attribute to that assembly, and pass in the value of the `PropertyGroup` variable from the previous step:  
    ```csproj
    <ItemGroup>
      <AssemblyAttribute Include="AssemblyAttribute.ExtendedVersion">
        <_Parameter1>$(ExtendedVersion)</_Parameter1>
        <!-- <_Parameter2></_Parameter2> -->
      </AssemblyAttribute>
    </ItemGroup>
    ```
 5. You can now retrieve the `ExtendedVersionAttribute` with the properties you passed in the csproj file in your code with:  
    ```csharp
    System.Reflection.Assembly.GetCallingAssembly().GetCustomAttribute<ExtendedVersionAttribute>();
    //                         ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
    //  Adapt as needed to get the assembly that corresponds to your csproj file!
    ```

Now all you have to do is update the version number in your `csproj` file **before** *(re)*building the solution somewhere in your build script *(This example uses PowerShell)*:  
/*```ps1
[xml]$CONTENT = Get-Content -Path "<YOUR_CSPROJ_FILE_PATH_HERE>"
$CONTENT.Project.PropertyGroup.ExtendedVersion = git describe --tags --abbrev=0
```
