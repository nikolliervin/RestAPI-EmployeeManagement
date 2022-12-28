# RestAPI-Kreatx
{
	"info": {
		"_postman_id": "7a26d307-b155-4471-b990-03d77e4e8fcf",
		"name": "REST API",
		"schema": "https://schema.getpostman.com/json/collection/v2.1.0/collection.json",
		"_exporter_id": "24887863"
	},
	"item": [
		{
			"name": "Login",
			"item": [
				{
					"name": "https://localhost:44309/api/login",
					"request": {
						"method": "POST",
						"header": [],
						"body": {
							"mode": "raw",
							"raw": "{\r\n    \"username\":\"user01\",\r\n    \"password\":\"Apiuser1.!\"\r\n}",
							"options": {
								"raw": {
									"language": "json"
								}
							}
						},
						"url": {
							"raw": "https://localhost:44309/api/login",
							"protocol": "https",
							"host": [
								"localhost"
							],
							"port": "44309",
							"path": [
								"api",
								"login"
							]
						},
						"description": "Logs the user in by passing a json object of UserLogin model"
					},
					"response": []
				},
				{
					"name": "https://localhost:44309/api/home/welcome",
					"protocolProfileBehavior": {
						"disableBodyPruning": true
					},
					"request": {
						"auth": {
							"type": "bearer",
							"bearer": [
								{
									"key": "token",
									"value": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidXNlcjAxIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiRW1wbG95ZWUiLCJleHAiOjE2NzE2NzUzMDAsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzA5LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzA5LyJ9.0Lx2Z2cm0dWJqH-t8LKh7JQhqn6NlutZ7PdhN4LzKHI",
									"type": "string"
								}
							]
						},
						"method": "GET",
						"header": [],
						"body": {
							"mode": "raw",
							"raw": "",
							"options": {
								"raw": {
									"language": "json"
								}
							}
						},
						"url": {
							"raw": "https://localhost:44309/api/home/welcome",
							"protocol": "https",
							"host": [
								"localhost"
							],
							"port": "44309",
							"path": [
								"api",
								"home",
								"welcome"
							]
						},
						"description": "Returns a string as a welcome message to confirm that the user is logged in"
					},
					"response": []
				}
			],
			"description": "The login area of the API that will be accessible to everyone."
		},
		{
			"name": "Admin",
			"item": [
				{
					"name": "Users",
					"item": [
						{
							"name": "Create User",
							"request": {
								"auth": {
									"type": "bearer",
									"bearer": [
										{
											"key": "token",
											"value": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjIiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTY3MjEyNjIyNSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzMDkvIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzMDkvIn0.SARLy_irm4Lopu5QNdoewxiWhCXJwwtpG7lD6dcAjpE",
											"type": "string"
										}
									]
								},
								"method": "POST",
								"header": [],
								"body": {
									"mode": "raw",
									"raw": "{\r\n    \"username\": \"test\",\r\n    \"password\":\"test\"\r\n}",
									"options": {
										"raw": {
											"language": "json"
										}
									}
								},
								"url": {
									"raw": "https://localhost:44309/api/admin/createuser",
									"protocol": "https",
									"host": [
										"localhost"
									],
									"port": "44309",
									"path": [
										"api",
										"admin",
										"createuser"
									]
								},
								"description": "Creates a new user of role \"Employee\""
							},
							"response": []
						},
						{
							"name": "DeleteUser",
							"request": {
								"method": "DELETE",
								"header": [],
								"url": {
									"raw": "https://localhost:44309/api/admin/deleteuser/{username}",
									"protocol": "https",
									"host": [
										"localhost"
									],
									"port": "44309",
									"path": [
										"api",
										"admin",
										"deleteuser",
										"{username}"
									]
								},
								"description": "Deletes the user by passing the username as a path parameter.\n\nReturns the user that got deleted if the user exists."
							},
							"response": []
						},
						{
							"name": "UpdateUser",
							"request": {
								"method": "PUT",
								"header": [],
								"body": {
									"mode": "raw",
									"raw": "{\r\n  \"username\": \"string\",\r\n  \"email\": \"user@example.com\",\r\n  \"phoneNumber\": \"string\",\r\n  \"firstName\": \"string\",\r\n  \"lastName\": \"string\",\r\n  \"profilePicture\": \"string\",\r\n  \"profilePictureUrl\": \"string\"\r\n}",
									"options": {
										"raw": {
											"language": "json"
										}
									}
								},
								"url": {
									"raw": "https://localhost:44309/api/admin/updateuser/username",
									"protocol": "https",
									"host": [
										"localhost"
									],
									"port": "44309",
									"path": [
										"api",
										"admin",
										"updateuser",
										"username"
									]
								},
								"description": "Updates the user by selecting the user by username as a path parameter and updates it to the json object passed in the body.\n\nReturns the updated user if the user was found."
							},
							"response": []
						}
					],
					"description": "The actions that the admin can take regarding users."
				},
				{
					"name": "Tasks",
					"item": [
						{
							"name": "CreateTask",
							"request": {
								"method": "POST",
								"header": [],
								"url": {
									"raw": "https://localhost:44309/api/admin/createtask/{user}/{project}",
									"protocol": "https",
									"host": [
										"localhost"
									],
									"port": "44309",
									"path": [
										"api",
										"admin",
										"createtask",
										"{user}",
										"{project}"
									]
								},
								"description": "Creates the task by passing two path parameters the user and the project. /user/project.\n\nReturns the created task if the project and the user exist."
							},
							"response": []
						},
						{
							"name": "UpdateTask",
							"request": {
								"method": "POST",
								"header": [],
								"body": {
									"mode": "raw",
									"raw": "{\r\n  \"taskName\": \"string\",\r\n  \"newTaskName\": \"string\",\r\n  \"newTaskDesc\": \"string\",\r\n  \"isFinished\": true\r\n}",
									"options": {
										"raw": {
											"language": "json"
										}
									}
								},
								"url": {
									"raw": "https://localhost:44309/api/admin/updatetask",
									"protocol": "https",
									"host": [
										"localhost"
									],
									"port": "44309",
									"path": [
										"api",
										"admin",
										"updatetask"
									]
								},
								"description": "Updates the task to the task that is passed from body as a json parameter. The task is identified by the field \"TaskName\" as the old task name.\n\nReturns the updated task if the task was found."
							},
							"response": []
						},
						{
							"name": "DeleteTask",
							"request": {
								"method": "DELETE",
								"header": [],
								"url": {
									"raw": "https://localhost:44309/api/admin/deletetask/{taskname}",
									"protocol": "https",
									"host": [
										"localhost"
									],
									"port": "44309",
									"path": [
										"api",
										"admin",
										"deletetask",
										"{taskname}"
									]
								},
								"description": "Deletes the task that is selected by passing taskname as a path parameter.\n\nReturns the task that just got deleted if that task was found."
							},
							"response": []
						},
						{
							"name": "AssingTaskTo",
							"request": {
								"method": "POST",
								"header": [],
								"url": {
									"raw": "https://localhost:44309/api/admin/assigntask/{taskname}/{username}",
									"protocol": "https",
									"host": [
										"localhost"
									],
									"port": "44309",
									"path": [
										"api",
										"admin",
										"assigntask",
										"{taskname}",
										"{username}"
									]
								},
								"description": "Assigns the task to a new employee. The taskname and employee username are selected by passing both as a path parameter.\n\nReturns the task if the employee and the task were found."
							},
							"response": []
						},
						{
							"name": "CompleteTask",
							"request": {
								"method": "POST",
								"header": [],
								"url": {
									"raw": "https://localhost:44309/api/admin/completetask/{task}",
									"protocol": "https",
									"host": [
										"localhost"
									],
									"port": "44309",
									"path": [
										"api",
										"admin",
										"completetask",
										"{task}"
									]
								},
								"description": "Marks the task as finished. The task is identified by inputing task name as a path url.\n\nReturns the task marked as finished if the task exists."
							},
							"response": []
						}
					],
					"description": "The actions the admin can take regarding the user tasks"
				},
				{
					"name": "Projects",
					"item": [
						{
							"name": "AddEmployeeToProject",
							"request": {
								"method": "POST",
								"header": [],
								"url": {
									"raw": "https://localhost:44309/api/admin/addtoproject/{employee}/{project}",
									"protocol": "https",
									"host": [
										"localhost"
									],
									"port": "44309",
									"path": [
										"api",
										"admin",
										"addtoproject",
										"{employee}",
										"{project}"
									]
								},
								"description": "Adds the employee to the project. Employee and the project are selected by being passed both as a path parameter.\n\nReturns an Ok result of a new record of the projectId and the employeeId."
							},
							"response": []
						},
						{
							"name": "RemoveEmployeeFromProject",
							"request": {
								"method": "DELETE",
								"header": [],
								"url": {
									"raw": "https://localhost:44309/api/admin/removemeployeefrom/{employee}/{project}",
									"protocol": "https",
									"host": [
										"localhost"
									],
									"port": "44309",
									"path": [
										"api",
										"admin",
										"removemeployeefrom",
										"{employee}",
										"{project}"
									]
								},
								"description": "Removes employee from a project. Employee and Project are selected by inputing them as path url.\n\nReturns the record of ProjectId and EmployeeId that just got deleted."
							},
							"response": []
						},
						{
							"name": "CreateProject",
							"request": {
								"method": "POST",
								"header": [],
								"body": {
									"mode": "raw",
									"raw": "{\r\n  \"name\": \"string\",\r\n  \"projectDesc\": \"string\"\r\n}",
									"options": {
										"raw": {
											"language": "json"
										}
									}
								},
								"url": {
									"raw": "https://localhost:44309/api/admin/addproject",
									"protocol": "https",
									"host": [
										"localhost"
									],
									"port": "44309",
									"path": [
										"api",
										"admin",
										"addproject"
									]
								},
								"description": "Creates a new project by passing it as a json object from body.\n\nReturns a json object of the created project."
							},
							"response": []
						},
						{
							"name": "UpdateProject",
							"request": {
								"method": "PUT",
								"header": [],
								"body": {
									"mode": "raw",
									"raw": "{\r\n  \"name\": \"string\",\r\n  \"projectDesc\": \"string\"\r\n}",
									"options": {
										"raw": {
											"language": "json"
										}
									}
								},
								"url": {
									"raw": "https://localhost:44309/api/admin/updateproject/{projectname}",
									"protocol": "https",
									"host": [
										"localhost"
									],
									"port": "44309",
									"path": [
										"api",
										"admin",
										"updateproject",
										"{projectname}"
									]
								},
								"description": "Updates a project by selecting it by project name passed as a path parameter and updates that project to the project passed in body as a json object.\n\nReturns the json object of the updated project if the project was found."
							},
							"response": []
						},
						{
							"name": "DeleteProject",
							"request": {
								"method": "DELETE",
								"header": [],
								"url": {
									"raw": "https://localhost:44309/api/admin/removeproject/{projectname}",
									"protocol": "https",
									"host": [
										"localhost"
									],
									"port": "44309",
									"path": [
										"api",
										"admin",
										"removeproject",
										"{projectname}"
									]
								},
								"description": "Deletes a project that is selected by project name that is passed as a path parameter.\n\nReturns a json object of the project that just got deleted if the project existed."
							},
							"response": []
						}
					],
					"description": "The actions the admin can take regarding the projects."
				}
			],
			"description": "The admin area. Only users of role \"Admin\" can access it."
		},
		{
			"name": "Employee",
			"item": [
				{
					"name": "Profile",
					"item": [
						{
							"name": "GetProfileData",
							"request": {
								"auth": {
									"type": "bearer",
									"bearer": [
										{
											"key": "token",
											"value": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidXNlcjAxIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiRW1wbG95ZWUiLCJleHAiOjE2NzIxNTg4NTksImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzA5LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzA5LyJ9.tozNukghsSEAbHKxEIhHBd6RgUHqU06lmKCCUSmJ2XA",
											"type": "string"
										}
									]
								},
								"method": "GET",
								"header": [],
								"url": {
									"raw": "https://localhost:44309/api/employee/profile",
									"protocol": "https",
									"host": [
										"localhost"
									],
									"port": "44309",
									"path": [
										"api",
										"employee",
										"profile"
									]
								},
								"description": "The employee can see their profile data such as personal info, projects and tasks they are part of."
							},
							"response": []
						},
						{
							"name": "ChangeProfilePicture",
							"request": {
								"auth": {
									"type": "bearer",
									"bearer": [
										{
											"key": "token",
											"value": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidXNlcjAxIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiRW1wbG95ZWUiLCJleHAiOjE2NzIxNTg4NTksImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzA5LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzA5LyJ9.tozNukghsSEAbHKxEIhHBd6RgUHqU06lmKCCUSmJ2XA",
											"type": "string"
										}
									]
								},
								"method": "POST",
								"header": [],
								"body": {
									"mode": "raw",
									"raw": "{\r\n  \"name\": \"profilepicture.png\",\r\n  \"fileUrl\": \"/home/newfolder/picture.png\"\r\n}",
									"options": {
										"raw": {
											"language": "json"
										}
									}
								},
								"url": {
									"raw": "https://localhost:44309/api/employee/updateprofilepicture",
									"protocol": "https",
									"host": [
										"localhost"
									],
									"port": "44309",
									"path": [
										"api",
										"employee",
										"updateprofilepicture"
									]
								},
								"description": "Updates the profile picture of the employee by passing a json object of type ProfilePicture containing the name of the file and the path.\n\nRetruns a json object of the new profile picture if the model was passed correctly."
							},
							"response": []
						},
						{
							"name": "UpdateProfileData",
							"request": {
								"method": "POST",
								"header": [],
								"body": {
									"mode": "raw",
									"raw": "{\r\n  \"firstName\": \"string\",\r\n  \"lastName\": \"string\",\r\n  \"phoneNumber\": \"string\"\r\n}",
									"options": {
										"raw": {
											"language": "json"
										}
									}
								},
								"url": {
									"raw": "https://localhost:44309/api/employee/updateprofiledata",
									"protocol": "https",
									"host": [
										"localhost"
									],
									"port": "44309",
									"path": [
										"api",
										"employee",
										"updateprofiledata"
									]
								},
								"description": "Updates employee personal data. Returns an Ok result and json object containing the same data if the model was passed correctly."
							},
							"response": []
						}
					],
					"description": "Actions employees will take regarding their profile."
				},
				{
					"name": "Tasks",
					"item": [
						{
							"name": "CreateTask",
							"request": {
								"auth": {
									"type": "bearer",
									"bearer": [
										{
											"key": "token",
											"value": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidXNlcjAxIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiRW1wbG95ZWUiLCJleHAiOjE2NzIxNTkyMDgsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzA5LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzA5LyJ9.GKI6ZYK_hxwS2JRXb_OUvwY_V5qieEpnn8louhyrx3A",
											"type": "string"
										}
									]
								},
								"method": "POST",
								"header": [],
								"body": {
									"mode": "raw",
									"raw": "{\r\n  \"taskName\": \"API Documentation\",\r\n  \"taskDesc\": \"Write Api documentation\",\r\n  \"isFinished\": false\r\n}",
									"options": {
										"raw": {
											"language": "json"
										}
									}
								},
								"url": {
									"raw": "https://localhost:44309/api/employee/createtask/Rest api",
									"protocol": "https",
									"host": [
										"localhost"
									],
									"port": "44309",
									"path": [
										"api",
										"employee",
										"createtask",
										"Rest api"
									]
								},
								"description": "Creates a task on a certain project that is passed as a path parameter. The task is passed as a json object from body.\n\nReturns an Ok result containing a json object of the task that was just created if the model was passed correctly and the project exists."
							},
							"response": []
						},
						{
							"name": "AssignTaskTo",
							"request": {
								"auth": {
									"type": "bearer",
									"bearer": [
										{
											"key": "token",
											"value": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidXNlcjAxIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiRW1wbG95ZWUiLCJleHAiOjE2NzIxNTkyMDgsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzA5LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzA5LyJ9.GKI6ZYK_hxwS2JRXb_OUvwY_V5qieEpnn8louhyrx3A",
											"type": "string"
										}
									]
								},
								"method": "POST",
								"header": [],
								"body": {
									"mode": "raw",
									"raw": "{\r\n  \"username\": \"user03\",\r\n  \"taskName\": \"API Documentation\"\r\n}",
									"options": {
										"raw": {
											"language": "json"
										}
									}
								},
								"url": {
									"raw": "https://localhost:44309/api/employee/assigntaskto",
									"protocol": "https",
									"host": [
										"localhost"
									],
									"port": "44309",
									"path": [
										"api",
										"employee",
										"assigntaskto"
									]
								},
								"description": "Assigns the task to another employee. Employee and the task are selected by the json object passed from body.\n\nReturns an Ok result containing the same object if the task and the user were found and the aimed user was part of the same project."
							},
							"response": []
						},
						{
							"name": "CompleteTask",
							"request": {
								"auth": {
									"type": "bearer",
									"bearer": [
										{
											"key": "token",
											"value": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidXNlcjAxIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiRW1wbG95ZWUiLCJleHAiOjE2NzIxNTkyMDgsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzA5LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzA5LyJ9.GKI6ZYK_hxwS2JRXb_OUvwY_V5qieEpnn8louhyrx3A",
											"type": "string"
										}
									]
								},
								"method": "PUT",
								"header": [],
								"url": {
									"raw": "https://localhost:44309/api/employee/finishtask/NewTaskName",
									"protocol": "https",
									"host": [
										"localhost"
									],
									"port": "44309",
									"path": [
										"api",
										"employee",
										"finishtask",
										"NewTaskName"
									]
								},
								"description": "Marks task as finished by inputing task name as a path url.\n\nReturns an Ok result containing the task if the task was found."
							},
							"response": []
						},
						{
							"name": "GetAllTasks",
							"request": {
								"auth": {
									"type": "bearer",
									"bearer": [
										{
											"key": "token",
											"value": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidXNlcjAxIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiRW1wbG95ZWUiLCJleHAiOjE2NzIxNTkyMDgsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzA5LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzA5LyJ9.GKI6ZYK_hxwS2JRXb_OUvwY_V5qieEpnn8louhyrx3A",
											"type": "string"
										}
									]
								},
								"method": "GET",
								"header": [],
								"url": {
									"raw": "https://localhost:44309/api/employee/watchalltasks",
									"protocol": "https",
									"host": [
										"localhost"
									],
									"port": "44309",
									"path": [
										"api",
										"employee",
										"watchalltasks"
									]
								},
								"description": "Gets all the tasks from the project the employee is part of."
							},
							"response": []
						},
						{
							"name": "UpdateTask",
							"request": {
								"auth": {
									"type": "bearer",
									"bearer": [
										{
											"key": "token",
											"value": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidXNlcjAxIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiRW1wbG95ZWUiLCJleHAiOjE2NzIxNTkyMDgsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzA5LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzA5LyJ9.GKI6ZYK_hxwS2JRXb_OUvwY_V5qieEpnn8louhyrx3A",
											"type": "string"
										}
									]
								},
								"method": "PUT",
								"header": [],
								"body": {
									"mode": "raw",
									"raw": "{\r\n  \"taskName\": \"TestTask\",\r\n  \"taskDesc\": \"TestDesc\",\r\n  \"isFinished\": false\r\n}",
									"options": {
										"raw": {
											"language": "json"
										}
									}
								},
								"url": {
									"raw": "https://localhost:44309/api/employee/updatetask/TaskName",
									"protocol": "https",
									"host": [
										"localhost"
									],
									"port": "44309",
									"path": [
										"api",
										"employee",
										"updatetask",
										"TaskName"
									]
								},
								"description": "Updates the task that is selected by task name as a path parameter. The selected task is updated to the task passed from body."
							},
							"response": []
						}
					],
					"description": "The actions the emplyee can take regarding tasks."
				}
			],
			"description": "The employee area which will be accessed by users of role \"Employee\"."
		}
	]
}
