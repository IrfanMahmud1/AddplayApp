import CreateUserForm from "./components/CreateUserForm";
import UserTable from "./components/UserTable";

export default function App() {
  return (
    <div className="p-4">
      <h2>User Management</h2>
      <CreateUserForm />
      <hr />
      <UserTable />
    </div>
  );
}
