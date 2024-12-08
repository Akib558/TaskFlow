<!-- 
    TODO: User Role on each task
    TODO: A new Project Entity needed, for project based task, user roles, etc
    TODO: Group Based Task can be have 
-->

---

### Core Database Tables

1. * *Users * *
   -**Id * *: Primary key.
   -**Name * *: User's full name.
   - **Email**: Unique email address.
   - **PasswordHash**: For authentication.
   -**Role * *: Global roles like Admin, Guest, etc.

2. **Projects**
   - **Id**: Primary key.
   -**Name * *: Project name.
   -**Description * *: Details about the project.
   - **StartDate**: When the project starts.
   - **EndDate**: Expected end date.
   - **Status**: Active, Completed, On Hold, etc.
   - **CreatedBy**: Foreign key to `Users.Id`.

3. **Tasks**
   - **Id**: Primary key.
   -**Title * *: Task title.
   -**Description * *: Task details.
   -**ProjectId * *: Foreign key to `Projects.Id`.
   - **Status**: Pending, In Progress, Done, etc.
   - **Priority**: Low, Medium, High.
   - **CreatedBy**: Foreign key to `Users.Id` (creator).
   - **CreatedDate**: Date of creation.
   - **DueDate**: Deadline for the task.

4. **TaskAssignments**
   - **Id**: Primary key.
   -**TaskId * *: Foreign key to `Tasks.Id`.
   - **UserId**: Foreign key to `Users.Id`.
   - **Role**: Role in this task (e.g., Developer, Reviewer).

5. **ProjectRoles**
   - **Id**: Primary key.
   -**Name * *: Role name(Manager, SQA, Developer, etc.).

6. * *ProjectMembers * *
   -**Id * *: Primary key.
   -**UserId * *: Foreign key to `Users.Id`.
   - **ProjectId**: Foreign key to `Projects.Id`.
   - **RoleId**: Foreign key to `ProjectRoles.Id`.

7. **TaskUpdates**
   - **Id**: Primary key.
   -**TaskId * *: Foreign key to `Tasks.Id`.
   - **UpdatedBy**: Foreign key to `Users.Id`.
   - **UpdateType**: Added Assignee, Status Change, etc.
   - **Details**: JSON or string to store update details.
   - **Timestamp**: When the update occurred.

---

### Optional Tables for Advanced Features

1. **Labels**
   - **Id**: Primary key.
   -**Name * *: Label name(e.g., Bug, Feature, Enhancement).

2. * *TaskLabels * *
   -**TaskId * *: Foreign key to `Tasks.Id`.
   - **LabelId**: Foreign key to `Labels.Id`.

3. **Comments**
   - **Id**: Primary key.
   -**TaskId * *: Foreign key to `Tasks.Id`.
   - **UserId**: Foreign key to `Users.Id`.
   - **Content**: Comment text.
   -**Timestamp * *: When the comment was added.

4. **Attachments**
   - **Id**: Primary key.
   -**TaskId * *: Foreign key to `Tasks.Id`.
   - **FilePath**: Location of the file.

---

### Relations Overview

- **Users** can create tasks and projects.
- A **Project** has multiple **Tasks**.
- A **Task** can have multiple **TaskAssignments** for collaboration.
- **Users** can have different roles in a **Project**.
- **TaskUpdates** track changes made to tasks.

---

### Next Steps

1. **Start Small**: Implement core tables like `Users`, `Projects`, and `Tasks` first.
2. **Basic Features**: Allow task creation, project linking, and simple task assignment.
3. **Iterate**: Gradually add advanced features like roles, updates, and comments.
4. **Documentation**: Use Swagger to document API endpoints as you go.

---
