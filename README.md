# order-management-system
**A simple order management system developed using .NET and WPF.**

---

Multi-project C# solution created as a project early on in my studies. Allows the creation, updating and processing of orders, as well as stock management. 

---

![Making a new order](https://res.cloudinary.com/dkj7bctqg/image/upload/v1621571365/Projects/OMS/new_order_psjd7p.png)

---

### Architecture:
WPF + .NET Framework + SQL Server

---

**UI => Controllers => DAL => Database**
*=> Domain <=*

The Domain project houses the business entities, referenced by both the Controller Layer and Data Access Layer. The UI is a Windows Presentation Foundation project. Events from the UI are passed to the Order and Stock controllers, which model business logic. The controllers use corresponding repos in DAL to execute queries and non-queries against the SQL Server database. 

If I had to give it a name, I'd go for 'New Order'.
