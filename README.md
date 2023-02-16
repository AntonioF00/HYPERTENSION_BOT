# HYPERTENSION BOT PROJECT
This is a project three-year degree in [Applied Computer Science](https://informatica.uniurb.it/) realized in collaboration with the [University of Urbino "Carlo Bo"](https://www.uniurb.it/). Professor [Sara Montagna](https://www.uniurb.it/persone/sara-montagna) and Professor [Cuno Lorenz Klopfenstein](https://www.uniurb.it/persone/cuno-klopfenstein) is following me in this pilot project.

## Description
This project is aimed at helping [Dr. Martino Pengo](https://www.auxologico.it/equipe/dott-martino-pengo) and the problem that has afflicted the world for several years, namely [arterial hypertension](https://www.marionegri.it/magazine/ipertensione-arteriosa?gclid=CjwKCAiA85efBhBbEiwAD7oLQOezC4HCCgRSeiOY6Uz37gq6RZE-66ushhO-qMO1IpgHw4-OOYTKNhoCA3cQAvD_BwE). The project was developed in [.NET 6](https://dotnet.microsoft.com/en-us/), there is [Postgres](https://www.postgresql.org/) as SQL Database and [Telegram API](https://core.telegram.org/) lets are used.
The main aim of the project is to develop a [ChatBot](https://www.oracle.com/it/chatbots/what-is-a-chatbot/) that helps patients suffering from this disease in the daily entry of data relating to their blood pressure. In recent years, ChatBots have become more and more popular and have proved to be a trump card in the medical field.

## Installation
### Windows 8/10/11 and MacOS
#### Install the SDK
The .NET SDK allows you to develop apps with .NET. If you install the .NET SDK, you don't need to install the corresponding runtimes. To install the .NET SDK, run the following command:

```bash
Windows 8/10/11:
winget install Microsoft.DotNet.SDK.7
```

```bash
MacOS:
brew install dotnet
```

#### Install the runtime .NET Desktop Runtime
For Windows, there are three .NET runtimes you can install. You should install both the .NET Desktop Runtime and the ASP.NET Core Runtime to ensure that you're compatible with all types of .NET apps.

This runtime includes the base .NET runtime, and supports Windows Presentation Foundation (WPF) and Windows Forms apps that are built with .NET. This isn't the same as .NET Framework, which comes with Windows.

```bash 
Windows 8/10/11:
winget install Microsoft.DotNet.DesktopRuntime.7
```

#### Install latest version of PostgreSQL
[Download link](https://www.postgresql.org/download/)

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)
MIT License

Copyright (c) [2023] [hypertension_bot]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
