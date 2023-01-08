# Overview

The purpose of this RestAPI is to manage Emoployees, Tasks and Projects.
The employees can have many tasks and be part of many projects.
The authentication is done with JSON Web Token and the users are managed by ASP.NET Core Identity.

###### The API users have two different roles: 
> Admin and
> Employee

## Employees can:
1. Watch their profile data
2. Update their profile data
3. Update their profile picture
4. Create tasks in the projects they are part of
5. Update tasks they have
6. Assign a task they have to another employee
7. Mark a task as finished
8. Watch all tasks from the projects they are part of.

## Admins can:
1. Create Users
2. Update Users
3. Delete Users

4. Create Tasks
5. Update Tasks
6. Delete Tasks

7. Add employees to projects
8. Delete employees from projects

9. Assign tasks to employees
10. Mark tasks as completed
11. Remove tasks

12. Create Projects
13. Update Projects
14. Delete Projects.

# Postman Documentation:

The full postman documentation can be found [Here](https://documenter.getpostman.com/view/24887863/2s8Z6x4a19)


# Accounts

Admin: <br/>
Username: admin <br/>
Password: Apiuser1.! <br/>

Employees: <br/>
user01 <br/>
user03 <br/>
user04 <br/>
user08 <br/>
Password: Apiuser1.! <br/>
