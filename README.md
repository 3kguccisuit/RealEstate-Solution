# Estate Management System

A WPF application designed to manage estates, buyers, sellers, and payments, initially created as part of a school project. The system provides a robust foundation for managing different types of estates with plans for future expansions and enhanced data handling.

![Estate Management Screenshot](https://github.com/3kguccisuit/RealEstate-Solution/blob/master/EstateListView.png)
## Features
- **Estate Management**: Create, update, and delete estates (Apartment, Villa, Townhouse, etc.) with specific attributes.
- **Person Management**: Handles buyer and seller data, including attributes like name, address, budget, loan approval, and asking price.
- **Payment System**: Create, update, and delete payments through various methods such as Bank transfers, Western Union, and PayPal. Each payment option is represented by different attributes like account number or email.
- **Data Services**: JSON-based data persistence for estates, people, and payments using a service pattern.
- **Modern UI**: Utilizes MahApps.Metro and WPF-UI for a modern and fluent user experience.
- **Dynamic Binding**: Estate types are determined at runtime based on user input.
- **Data Persistence**: Save and load data using JSON serialization.

## Installation

### Prerequisites:
- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or later)
- [Visual Studio](https://visualstudio.microsoft.com/) with WPF and .NET desktop development workloads.

### Steps:
1. Clone the repository:
   ```bash
   git clone https://github.com/3kguccisuit/RealEstate-Solution
2. Open the solution in Visual Studio.
3. Restore NuGet packages.
4. Build and run the project. 

## Technologies Used
- **MVVM Framework**: [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/MVVM)
- **UI Framework**: [MahApps.Metro](https://github.com/MahApps/MahApps.Metro), [WPF-UI](https://github.com/lepoco/WPFUI)
- **JSON Serialization**: [Newtonsoft.Json](https://www.newtonsoft.com/json), [System.Text.Json](https://docs.microsoft.com/en-us/dotnet/api/system.text.json)
- **Logging**: [Serilog](https://serilog.net/)
- **Project Setup**: [Built using Template Studio](https://marketplace.visualstudio.com/items?itemName=TemplateStudio.TemplateStudioForWPF) 



