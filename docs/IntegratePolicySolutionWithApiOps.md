# Integrate policy solution with repository containing data for APIOps

Azure API Management policy toolkit is very elastic and allow a developer to define how the structure of repository will
look. In this guide we will touch how toolkit project can produce policies which can be easily published
by [APIOps tool](https://azure.github.io/apiops) to Azure API Management instance.

## APIOps repository structure

Repository with Azure API Management data created by APIOps tool should have structure similar to below folder tree.
We included folders and files about policies in the repository, and we skipped folders and files not relevant in the
example.

```
.
├── artifacts
│    ├── policy.xml
│    ├── apis
│    │   └── echo-api
│    │       ├── policy.xml
│    │       └── operations
│    │           ├── get
│    │           │   └── policy.xml
│    │           └── post
│    │               └── policy.xml
│    └── products
│        └── Unlimited
│            └── policy.xml
└── configuration.dev.yaml
```

## Adding policy project to the repository

Best way to add policy project to the repository is to add it as a subfolder. This way the policy project can separate
from the APIOps data and can be easily updated and maintained. In the example below we added the policy project to the
`policies` folder. We added a dotnet tool to be available from the root of the repository
(`./.config/dotnet-tools.json`).

```
.
├── artifacts
│    ├── policy.xml
│    ├── apis
│    │   └── echo-api
│    │       ├── policy.xml
│    │       └── operations
│    │           ├── get
│    │           │   └── policy.xml
│    │           └── post
│    │               └── policy.xml
│    └── products
│        └── Unlimited
│            └── policy.xml
├── configuration.dev.yaml
├── .config
│   └── dotnet-tools.json
└── policies
    ├── policies.sln
    ├── src
    │   ├── src.csproj
    │   ├── apis
    │   │   └── echo-api
    │   │       ├── ApiEchoApiPolicy.cs
    │   │       └── operations
    │   │           ├── get
    │   │           │   └── ApiEchoApiGetPolicy.cs
    │   │           └── post
    │   │               └── ApiEchoApiPostPolicy.cs
    │   └── products
    │       └── Unlimited
    │           └── ProductUnlimitedPolicy.cs
    └── test 
        ├── test.csproj
        ...
```

## Setting name of the policy file

APIOps accepts policies when they are named `policy.xml`. The toolkit compiler by default produces file names equal to
the name of a class when name is not provided in `Document` attribute. To make sure that `policy.xml` file is produce
for
each C# policy class make sure to set `policy.xml` as Document name.

```csharp
[Document("policy.xml")]
```

## Updating policy files in APIOps artifacts folder

Now as we have all the pieces in place we can run the compiler. Compiler should target `artifacts` folder with APIOps
data. Example of the command to run the compiler is below.

```shell
dotnet policy-compiler --s .\policies\src\ --o .\artifacts\
```

After the command is executed the `artifacts` folder should contain all the policy files which can be easily published
by APIOps tool to Azure API Management instance.