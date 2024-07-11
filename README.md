# ASP.NET Job Distribution and Management Website with Particle Swarm Analysis (PSO)

This is a web application built using ASP.NET and C# that enables Job Distribution and Management using Particle Swarm Analysis (PSO). The website consists of four types of pages: Workers, Machines, Materials, and Historical Job Logging. Each page displays different datasets that are used in PSO.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)
- [Reference](#reference)
- [License](#license)

## Introduction

The ASP.NET Job Distribution and Management website is designed to facilitate the distribution and management of jobs using the PSO algorithm. PSO is a population-based optimization algorithm that mimics the behavior of a swarm of particles. It is commonly used in optimization problems, and in this application, it helps in efficient job distribution among workers, machines, and materials.

## Features

- **Workers Page:** This page displays the dataset of available workers. It allows administrators to add, edit, or remove workers from the system.

- **Machines Page:** This page provides information about the available machines for job execution. It includes details such as machine specifications and availability. Administrators can manage the machines only by edit the units.

- **Materials Page:** The materials page showcases the dataset of available materials required for job execution. It includes details such as material properties, quantity, and availability. Administrators can manage the materials only by edit the units.

- **Historical Job Logging Page:** This page presents a historical log of completed jobs. It provides insights into job execution, including details such as job ID, request date, submission date, start date, finish date, assigned worker ID, machine used, material used.

## Prerequisites

To run the ASP.NET Job Distribution and Management website, ensure that you have the following prerequisites installed:

- Visual Studio with ASP.NET and C# support
- .NET Framework (version X.X or higher)
- Web browser (Chrome, Firefox, etc.)
- SQL for database (need to create by your own self under sqlexpress tables), In this case we have 4 types of tables

## Installation

1. Clone the repository or download the source code files.

2. Open the project in Visual Studio.

3. Build the project to restore the necessary packages and dependencies.

4. Install nugget package : System.Data.SqlClient, System.Text.Encoding.CodePages

## Usage

1. Launch the ASP.NET website from Visual Studio or publish it to a web server.

2. Access the website using a web browser.

3. Navigate through the different pages: Workers, Machines, Materials, and Historical Job Logging.

4. Perform actions such as adding, editing, or removing workers, machines, and materials as required.

5. Monitor job distribution and management using the PSO algorithm.

## Contributing

Contributions to the ASP.NET Job Distribution and Management website are welcome. If you find any bugs, have suggestions for improvements, or would like to add new features, please submit a pull request.

## Reference

1. https://youtu.be/T-e554Zt3n4 (Create ASP.NET Core Web Application With SQL Server Database Connection and CRUD Operations)

2. https://youtu.be/qFNZNFw_Wf8 (How to Find the Instance Name and the Server Name of Microsoft SQL Server)

## License

This project is licensed under the [MIT License](LICENSE). Feel free to modify and use the code for personal or commercial purposes.

---

*Note: The above information is a general template for a README.md file. Please customize it as per your specific application and requirements.*
