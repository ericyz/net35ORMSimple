---
# .Net35 Simple ORM
-----------------
### Objective
The purpose of this library is to provide a simple solution for 3.5 or lower .Net version, which takes long time to configure with ORM such as Entity Framework and NHibernate.

### Features
- Provide a utility class to generate model and data access skeleton objects based on a SQL
- Easy implementation for read operation by key
- Provide annotations to customize model field name and to support composite key in database

### Project Directory
- SimpleORM: Core Library
- SimpleORM.Test: Unit Test Project for SimpleORM
- SampleApplication: A simple .Net3.5 Windows Form application to demonstrate the CRUD operation for Employees.

### How to use
In this section, how to generate the model of Employee.cs and data access object of EmployeeService.cs will be demonstrated.
- Include SimpleORM dll into the target project (Simple.ORM.Test for example)
- Invoke ORMUtil.GenreateClass(string connectionString, string sql). Please refer to GenerateClassTest method in SimpleORMTest.cs
- When both sql and connection string are valid, the data access object and model object will be created in the directory where it compiles.
- Include them into the target object (SimpleApplication for example)
- Add Key/DatabaseGenerated annotations when necessary
- Modify the class name and column name in Model when necessary (Optional)

### Annotation Used
- Key: Primary key which is not auto-increment in database table
- DatabaseGenerated: Indicate an auto-increment primary key, such as IDENTITY in SQL server
### Supported Database
- SQL Server

### Implementation
- C# Reflection
