# ![ProtonBlazor Logo](content/ProtonBlazor-GitHub-NoBg-Dark.png)

# Material Design components for Blazor
![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/protonblazor/protonblazor/build-test-protonblazor.yml?branch=dev&logo=github&style=flat-square)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=ProtonBlazor_ProtonBlazor&metric=alert_status)](https://sonarcloud.io/summary/overall?id=ProtonBlazor_ProtonBlazor)
[![Codecov](https://img.shields.io/codecov/c/github/ProtonBlazor/ProtonBlazor)](https://app.codecov.io/github/ProtonBlazor/ProtonBlazor)
[![GitHub](https://img.shields.io/github/license/protonblazor/protonblazor?color=594ae2&logo=github&style=flat-square)](https://github.com/protonblazor/ProtonBlazor/blob/master/LICENSE)
[![GitHub Repo stars](https://img.shields.io/github/stars/protonblazor/protonblazor?color=594ae2&style=flat-square&logo=github)](https://github.com/protonblazor/ProtonBlazor/stargazers)
[![Contributors](https://img.shields.io/github/contributors/protonblazor/protonblazor?color=594ae2&style=flat-square&logo=github)](https://github.com/protonblazor/protonblazor/graphs/contributors)
[![Discussions](https://img.shields.io/github/discussions/protonblazor/protonblazor?color=594ae2&logo=github&style=flat-square)](https://github.com/protonblazor/protonblazor/discussions)
[![Discord](https://img.shields.io/discord/786656789310865418?color=%237289da&label=Discord&logo=discord&logoColor=%237289da&style=flat-square)](https://discord.gg/protonblazor)
[![Twitter](https://img.shields.io/twitter/follow/ProtonBlazor?color=1DA1F2&label=Twitter&logo=Twitter&style=flat-square)](https://twitter.com/ProtonBlazor)
[![NuGet version](https://img.shields.io/nuget/v/ProtonBlazor?color=ff4081&label=nuget%20version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/ProtonBlazor/)
[![NuGet downloads](https://img.shields.io/nuget/dt/ProtonBlazor?color=ff4081&label=nuget%20downloads&logo=nuget&style=flat-square)](https://www.nuget.org/packages/ProtonBlazor/)

ProtonBlazor is an ambitious Material Design component framework for Blazor with an emphasis on ease of use and clear structure. It is perfect for .NET developers who want to rapidly build web applications without having to struggle with CSS and Javascript. ProtonBlazor, being written entirely in C#, empowers you to adapt, fix or extend the framework. There are plenty of examples in the documentation, which makes understanding and learning ProtonBlazor very easy.

**🌐 [Documentation](https://protonblazor.com/docs/overview) ⚡ [Interactive Playground](https://try.protonblazor.com)**

## 💎 Why Choose ProtonBlazor?

- Clean and aesthetic graphic design based on Material Design.
- Clear and easy to understand structure.
- Good documentation with many examples and source snippets.
- All components are written entirely in C#, no JavaScript allowed (except where absolutely necessary).
- Users can make beautiful apps without needing CSS (but they can of course use CSS too).
- No dependencies on other component libraries, 100% control over components and features.
- Stability! We strive for a complete test coverage.
- Releasing often so developers can get their PRs and fixes in a timely fashion.

## 📊 Repo Stats

![Repobeats analytics image](https://repobeats.axiom.co/api/embed/db53a44092e88fc34a4c0f37db12773b6787ec7e.svg)

## 🚀 Getting Started

See the [installation guide](https://protonblazor.com/getting-started/installation) to get started.

### Example Usage

```razor
<ProText Typo="Typo.h6">
    ProtonBlazor is @Text
</ProText>

<ProButton Variant="Variant.Filled" 
           Color="Color.Primary" 
           OnClick="ButtonOnClick">
    @ButtonText
</ProButton>

@code {
    string Text { get; set; } = "????";
    string ButtonText { get; set; } = "Click Me";
    int ClickCount { get; set; }

    void ButtonOnClick()
    {
        ClickCount += 1;
        Text = $"Awesome x {ClickCount}";
        ButtonText = "Click Me Again";
    }
}
```

## 🤝 Contributing

Contributions from the community are what make ProtonBlazor successful.  

💬 Feel free to chat with us [on Discord](https://discord.gg/protonblazor) to get feedback before diving in.  
📚 Check out our [contribution guidelines](/CONTRIBUTING.md) to get started and learn more about how the project works.  
🧪 If a PR fixes something you reported, [locally test it](https://github.com/ProtonBlazor/ProtonBlazor/discussions/12085) to ensure your app works as expected.

## ⚙️ Version Support

| ProtonBlazor | .NET | Support |
| :--- | :---: | :---: |
| 5.x.x | .NET 5 | Ended Jan 2022 |
| 6.x.x | [.NET 6](https://dotnet.microsoft.com/download/dotnet/6.0), [.NET 7](https://dotnet.microsoft.com/download/dotnet/7.0), [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0) | Ended Jan 2025 |
| 7.x.x | [.NET 7](https://dotnet.microsoft.com/download/dotnet/7.0), [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0) | Ended Jan 2026 |
| 8.x.x | [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0), [.NET 9](https://dotnet.microsoft.com/download/dotnet/9.0) | Limited Support |
| 9.x.x | [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0), [.NET 9](https://dotnet.microsoft.com/download/dotnet/9.0), [.NET 10](https://dotnet.microsoft.com/download/dotnet/10.0) | ✅ Full Support |

> [!NOTE]
> 1. Upgrading? Check our [Migration Guide](https://github.com/ProtonBlazor/ProtonBlazor/discussions/12086) for help with breaking changes.  
> 2. Static rendering is not supported. [Learn more](https://learn.microsoft.com/aspnet/core/blazor/components/render-modes)
> 3. Use an up-to-date browser. [Blazor supported platforms](https://learn.microsoft.com/aspnet/core/blazor/supported-platforms)
> 4. Want to test the latest features? Learn about our [nightly builds](https://github.com/ProtonBlazor/ProtonBlazor/discussions/12621)!
