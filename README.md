# Estate Management System

A WPF application designed to manage estates, buyers, sellers, and payments, initially created as part of a school project. The system provides a robust foundation for managing different types of estates with plans for future expansions and enhanced data handling.

## Features
- **Estate Management**: Create, update, and delete estates (Apartment, Villa, Townhouse, etc.) with specific attributes.
- **Person Management**: Handles buyer and seller data, including attributes like name, address, budget, loan approval, and asking price.
- **Payment System**: Manage payments through various methods such as Bank transfers, Western Union, and PayPal. Each payment option is represented by different attributes like account number or email.
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

## Project Structure
- **Estates**: Abstract classes for Institutional and Commercial properties, with concrete implementations like Hospital, School, Hotel, Shop, Warehouse, and Factory.
- **People**: Buyer and seller entities with attributes such as budget, loan approval, and asking price.
- **Payment System**: A flexible payment system with support for Bank transfers, Western Union, and PayPal.
- **Services**: Each entity type (estate, person, payment) has a dedicated data service handling CRUD operations via JSON.
- **Generics and Collections**: The application manages multiple estate objects using a collection of estates, enabling CRUD operations.

## Technologies Used
- **MVVM Framework**: [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/MVVM)
- **UI Framework**: [MahApps.Metro](https://github.com/MahApps/MahApps.Metro), [WPF-UI](https://github.com/lepoco/WPFUI)
- **JSON Serialization**: [Newtonsoft.Json](https://www.newtonsoft.com/json), [System.Text.Json](https://docs.microsoft.com/en-us/dotnet/api/system.text.json)
- **Logging**: [Serilog](https://serilog.net/)
- **Project Setup**: [Built using Template Studio](https://marketplace.visualstudio.com/items?itemName=TemplateStudio.TemplateStudioForWPF) 

## Future Plans
- **Add More Estate Types**: Expand the system with more estate categories such as office spaces and retail locations.
- **Enhanced Payment Management**: Extend the payment system with more advanced features such as transaction history.
- **Dictionary Usage**: Implement a search system that maps cities to estates using dictionary collections.
- **XML Serialization**: Future expansion to support XML serialization in addition to JSON for more flexible data handling.


