# task-management-rest-project
A project for Web-server languages discipline in "Paisii Hilendarski" University of Plovdiv

The API is documented via swagger at https://localhost:yourport/index.html. If you are more comfortable you can use the documentation below:

## Users API Documentation

### Endpoints

The controller offers various functionalities related to user management:

* **Create User:** Creates a new user in the system.
* **Update User Email:** Updates an existing user's email address.
* **Update User Password:** Changes a user's password.
* **Get User Info:** Retrieves information about a specific user.
* **Delete User:** Deletes an existing user from the system.

### Error Handling

All API endpoints return appropriate HTTP status codes to indicate success or failure:

* **200 OK:** The request was successful.
* **201 Created:** A new user was created successfully.
* **400 BadRequest:** The request was invalid due to missing or malformed data.
* **404 NotFound:** The requested user information was not found.
* **500 Internal Server Error:** An unexpected error occurred during processing.

### Endpoints Details

**1. Create User**

* **HTTP Method:** POST
* **URL Path:** `/users/create`
* **Request Body:** `CreateUserRequest` object
    * **Email:** Required string. Must be a valid email address.
    * **Password:** Required string. Minimum length of 6 characters.
* **Response:**
    * **201 Created:** User created successfully. No response body.

**2. Update User Email**

* **HTTP Method:** PUT
* **URL Path:** `/users/update/email`
* **Request Body:** `UpdateUserEmailRequest` object
    * **Id:** Required integer representing the user ID.
    * **NewEmail:** Required string. Must be a valid email address.
* **Response:**
    * **200 OK:** User email updated successfully. No response body.
    * **400 BadRequest:** The request is invalid (e.g., user not found, same email used).

**3. Update User Password**

* **HTTP Method:** PUT
* **URL Path:** `/users/update/password`
* **Request Body:** `UpdateUserPasswordRequest` object
    * **Id:** Required integer representing the user ID.
    * **Password:** Required string. Minimum length of 6 characters.
* **Response:**
    * **200 OK:** User password updated successfully. No response body.
    * **400 BadRequest:** The request is invalid (e.g., user not found, same password used).

**4. Get User Info**

* **HTTP Method:** GET
* **URL Path:** `/users/info`
* **Request Query:** Required integer parameter `id` specifying the user ID.
* **Response:**
    * **200 OK:** `UserInfoResponse` object containing user email and a list of tasks.
    * **404 NotFound:** The requested user was not found.

**5. Delete User**

* **HTTP Method:** DELETE
* **URL Path:** `/users/delete`
* **Request Query:** Required integer parameter `id` specifying the user ID.
* **Response:**
    * **204 No Content:** User deleted successfully. No response body.

## Projects API Documentation

### Endpoints

### Error Handling

All API endpoints return appropriate HTTP status codes to indicate success or failure:

* **200 OK:** The request was successful.
* **201 Created:** A new project was created successfully.
* **400 BadRequest:** The request was invalid due to missing or malformed data.
* **404 NotFound:** The requested project information was not found.
* **500 Internal Server Error:** An unexpected error occurred during processing.

### Endpoints Details

**1. Create Project**

* **HTTP Method:** POST
* **URL Path:** `/projects/create`
* **Request Body:** `CreateProjectRequest` object
    * **Name:** Required string representing the project name.
    * **Description:** Required string describing the project.
* **Response:**
    * **201 Created:** Project created successfully. No response body.

**2. Update Project**

* **HTTP Method:** PUT
* **URL Path:** `/projects/update`
* **Request Body:** `UpdateProjectRequest` object
    * **Id:** Required integer representing the project ID.
    * **Name:** Required string containing the updated project name.
    * **Description:** Required string with the updated project description.
* **Response:**
    * **200 OK:** Project updated successfully. No response body.
    * **404 NotFound:** The specified project was not found.

**3. Get Project Info**

* **HTTP Method:** GET
* **URL Path:** `/projects/info`
* **Request Query:** Required integer parameter `id` specifying the project ID.
* **Response:**
    * **200 OK:** `ProjectInfoResponse` object containing project details.
    * **404 NotFound:** The requested project was not found.

**4. Delete Project**

* **HTTP Method:** DELETE
* **URL Path:** `/projects/delete`
* **Request Query:** Required integer parameter `id` specifying the project ID.
* **Response:**
    * **204 No Content:** Project deleted successfully. No response body.

## Tasks API Documentation

### Endpoints

### Error Handling

All API endpoints return appropriate HTTP status codes to indicate success or failure:

* **200 OK:** The request was successful.
* **201 Created:** A new task was created successfully.
* **400 BadRequest:** The request was invalid due to missing or malformed data.
* **404 NotFound:** The requested task information was not found.
* **500 Internal Server Error:** An unexpected error occurred during processing.

### Endpoints Details

**1. Create Task**

* **HTTP Method:** POST
* **URL Path:** `/tasks/create`
* **Request Body:** `CreateTaskRequest` object
    * **OwnerId:** Required integer representing the owner's user ID.
    * **ProjectId:** Required integer representing the project ID.
    * **Title:** Required string containing the task title.
    * **Description:** Required string describing the task.
    * **Status:** Required string representing the task status (Todo, InProgress, Completed).
    * **DueDate (Optional):** DateTimeOffset object specifying the due date for the task.
* **Response:**
    * **201 Created:** Task created successfully. No response body.

**2. Update Task**

* **HTTP Method:** PUT
* **URL Path:** `/tasks/update`
* **Request Body:** `UpdateTaskRequest` object
    * **Id:** Required integer representing the task ID.
    * **Title:** Required string containing the updated task title.
    * **Description:** Required string with the updated task description.
    * **Status:** Required string representing the updated task status (Todo, InProgress, Completed).
    * **DueDate (Optional):** DateTimeOffset object specifying the updated due date for the task.
* **Response:**
    * **200 OK:** Task updated successfully. No response body.
    * **404 NotFound:** The specified task was not found.

**3. Get Task Info**

* **HTTP Method:** GET
* **URL Path:** `/tasks/info`
* **Request Query:** Required integer parameter `id` specifying the task ID.
* **Response:**
    * **200 OK:** `TaskInfoResponse` object containing task details.
    * **404 NotFound:** The requested task was not found.

**4. Delete Task**

* **HTTP Method:** DELETE
* **URL Path:** `/tasks/delete`
* **Request Query:** Required integer parameter `id` specifying the task ID.
* **Response:**
    * **204 No Content:** Task deleted successfully. No response body.

## Comments API Documentation

### Endpoints Details

**1. Get Task Comments**

* **HTTP Method:** GET
* **URL Path:** `/comments/for/task`
* **Request Query:** Required integer parameter `taskId` specifying the task ID.
* **Response:**
    * **200 OK:** Array of `Comment` objects representing the retrieved comments.
    * **404 NotFound:** The specified task was not found.

**2. Create Comment**

* **HTTP Method:** POST
* **URL Path:** `/comments/create`
* **Request Body:** `CreateCommentRequest` object
    * **TaskId:** Required integer representing the ID of the task to comment on.
    * **Content:** Required string containing the comment text.
* **Response:**
    * **201 Created:** Comment created successfully. No response body.

**3. Update Comment**

* **HTTP Method:** PUT
* **URL Path:** `/comments/update`
* **Request Body:** `UpdateCommentRequest` object
    * **Id:** Required integer representing the comment ID.
    * **Content:** Required string containing the updated comment text.
* **Response:**
    * **200 OK:** Comment updated successfully. No response body.
    * **400 BadRequest:** The request is invalid, potentially due to an unauthorized user attempting the update.

**4. Delete Comment**

* **HTTP Method:** DELETE
* **URL Path:** `/comments/delete`
* **Request Query:** Required integer parameter `id` specifying the comment ID.
* **Response:**
    * **204 No Content:** Comment deleted successfully. No response body.
