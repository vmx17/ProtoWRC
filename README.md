# ProtoWRC: Using DirectX in C\++ on C# XAML desktop

## Motivation
Hi! I'm trying to make desktop application for drawing.  
Microsoft says "C\++/WinRT is the best language for performance." and "C# is the best language for productivity.".  
Why don't we think of making hybrid application? I'm looking for such examples that drawing feature driven by DirectX in C\++, and for other part programmed in C#. It seems the best combination for such applications, isn't it?  

The "Microsoft Learn" (formerly "Microsoft Docs") provides some similar articles in _Reference_ section. But those are based on C\++ client and interop with C# dll.  
Why do I want to use C# for base framework? The answer is MVVM mostly. VisualStudio have `View` and `ViewModel` template for C\++ but I think C# still has some advantage for its libraries.  

As for DirectX, even now, almost tutorials start where make Windows (as Win32 app) and apart from XAML based new-face application.

Today, DirectX is [provided as COM component](https://learn.microsoft.com/en-us/windows/win32/prog-dx-with-com) (isn't it?). COM is exactly what I mention here. What is COM? Though there are many derivatives, COM is a binary format for API definition. Because COM defines binary format, every compilers — those support COM interface — can use it beyond programming languages.  

So we should be able to use it from C#.  

But I realized there are few articles about using C# to use DirectX and most of wrappers do not support all APIs. If we ask something, the wrapper authors help me kindly (thank you!). How in the future? I cannot keep depending on them.

So I returned to COM. Though I asked some questions to authors of "Microsoft Learn" (Those askings make GitHub issue), their answer is always "Ask Microsoft Q&A or Stack Overflow". Then I begun to ask "Microsoft Q&A", the answer came without new information. There are still question not answered after "escalated to expert team".  So my Try and Error has started. I put all C\++ code that draws on `SwapChainPanel` into single Windows Runtime Component (WRC) — a kind of COM package.

After all, here I show a rough answer. Using DirectX in C\++ but on C# XAML frame.
![SpinningCube](./images/SpinningCube.gif)  

## Usage
This is a set of VisualStudio 2022 solution file using .Net8. The solution filename is "ProtoWRC.sln". It has three projects.  
- "ProtoWRC": C\++ project that makes Windows Runtime Component (WRC) for DirectX except screen.
- "ProtoProjection": C# project for Projection. No `*.cs` code. (It will be made automatically)
- "WRCClient": C# project for application that consume DirectX back-end provided by "ProtoWRC".

(1) Open solution file.  
(2) restore NuGet packages for each project. (I recommend the newest stable release.)  
(3) Build `x64`, `Release` and/or `Debug`. (The "ProtoProjection" is fixed as `AnyCPU`.)  
(4) Run "WRCClient" with debugger. This is startup project.  
(5) at the top of screen, push "DirectX" button. It may be hidden under debugger menu. Shrink it (sorry for my layout design).  
(6) Now you can see spinning cube. That is drawn on `SwapChainPanel` located C# frame, by C\++ Windows Runtime Component. 
You may need to add [Single-project MSIX Packaging Tools](https://marketplace.visualstudio.com/items?itemName=ProjectReunion.MicrosoftSingleProjectMSIXPackagingToolsDev17) to your VisualStudio.

## What I did
Though I use many hours, nothing new nor special. Just put `SwapChainPanel` to interact between C# and C\++ because Windows App SDK for both language has the `UIElement` commonly.  
The interop is implemented by "project reference" through "Projection Project".  
Set debugger to enable native code debugging. (you can debug in both code by this.)  
I think these operations may be basic work for who make C# wrapper package.

## What I did not
- utilize methods prepared in `SwapChainPanel`. In this sample, I use it as just a draw target `Panel` for DirectX. `SwapChainPanel` seems contain some functions which can be used for drawing but I did not use it.

These should work according to WRC (MIDL3.0) specification.
- test event/delegate propagation
- test async command call
- make NuGet package in projection project.

These are ... Should I make by myself? There may be some workarounds.
- built into MVVM/C# architecture. Messenger service, IoC, DI...

## What's the pros and cons of this style?
pros:  
- can get performance and productivity. (I hope so.)
- with VisualStudio, I can use debugger to both langurage seamlessly.
- can use all DirectX libraries. For example, like [`DirectXMath`](https://learn.microsoft.com/en-us/windows/win32/dxmath/directxmath-portal), there are some portion implemented in pure C\++ code or static library. From C#, we cannot use them if the wrapper does not support. Especially I want `float2`, `float3`, `float4` and  `Inverse()` array/matrix functions. I need additional wrapper at the case. [JeremyAnsel.DirectX.DXMath](https://www.nuget.org/packages/JeremyAnsel.DirectX.DXMath) covers `DirectXMath` to some extent.
- expand possibility to use `double` or `doubleN` in application. Today, most of GPU support `double` in shader languages. (When DirectX wrapper supports only `float`.)
- no need to worry about wrappers support schedule / life cycle. (effective for the case SBOM is requested.)  

cons:  
- need to make code in both of C# and C++.
- The solution settings, software design become complicated. (but you can reduce somewhat by making NuGet packages.)
- the Projection Project have one layer overhead.

## Remaining Questions
- Though DirectX API should be able to be called directly from C# (because it is COM), I had to put a projection project. Are there any way to remove this? In "Microsoft Learn", they don't say it can.
- How large the cost of interaction? At least one layer is added by projection project.
- Are there any effective way to use `SwapChainPanel`'s methods at interop boundary?

## Known Issue
(I have no plan to fix.)  
- Some UI is not suit for screen.  
- Only "DirectX" button works. No other buttons are implemented.
- You may see RID warnings those come with .Net8.

## Reference
  
- [Generate a C# projection from a C\++/WinRT component, distribute as a NuGet for .NET apps](https://learn.microsoft.com/en-us/windows/apps/develop/platform/csharp-winrt/net-projection-from-cppwinrt-component)  
- [DirectX and XAML interop](https://learn.microsoft.com/en-us/windows/uwp/gaming/directx-and-xaml-interop)  
- [Author APIs with C\++/WinRT](https://learn.microsoft.com/en-us/windows/uwp/cpp-and-winrt-apis/author-apis)  
- [Introduction to Microsoft Interface Definition Language 3.0](https://learn.microsoft.com/en-us/uwp/midl-3/intro)
- [Consume COM components with C\++/WinRT](https://learn.microsoft.com/en-us/windows/uwp/cpp-and-winrt-apis/consume-com)
- [Walkthrough—Create a C#/WinRT component, and consume it from C\++/WinRT](https://learn.microsoft.com/en-us/windows/apps/develop/platform/csharp-winrt/create-windows-runtime-component-cswinrt)
- [Walkthrough—Create a C# component with WinUI 3 controls, and consume it from a C\++/WinRT app that uses the Windows App SDK](https://learn.microsoft.com/en-us/windows/apps/develop/platform/csharp-winrt/create-winrt-component-winui-cswinrt)
- [Authoring C#/WinRT Components](https://github.com/microsoft/CsWinRT/blob/master/docs/authoring.md)
  
## Special Thanks
  
- Simon Mourier:  The author of [DirectN](https://github.com/smourier/DirectN). (I no longer use DirectN in this project.)  
- Steven White:   the author of many articles linked above.  
- Emmett Tsai:    DirectX part in this sample comes from his [project](https://github.com/EmmettTsai/SpinningCube).
