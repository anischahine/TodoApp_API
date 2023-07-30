This is a simple backend (API) for a TodoApp, getting data from mongodb online database.(the database link was removed for policy)
The main idea was consisting on accounts and task related to these account.
The user can sign up using the front end side, access its dashboard with list of created, completed and archived tasks.

The routes used for Account Controller are:
- /api/Account/GetAll 'returns all accounts
- /api/Account/login/{email}/{password} 'login with email and password if the account is active
- /api/Account/signup 'Create a new account with email, password, full name
- /api/Account/update/{id} 'Update existing account information such as fullname or password or activate/desactivate account
- /api/Account/delete/{id} 'Delete existing account from database

The routes used for Task Controller are:
- /api/Task/GetAll 'returns all tasks
- /api/Task/getByEmailState/{email}/{state} 'Get tasks for a single user with task state/ created, completed or archived
- /api/Task/create 'Create a new task with task description, account email and state
- /api/Task/update/{id} 'Update existing task information such as description or task state
- /api/Task/delete/{id} 'Delete existing task from database
