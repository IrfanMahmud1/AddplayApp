import { useEffect, useState } from "react";
import { fetchUsers } from "../api/userApi";

export default function UserTable() {
  const [users, setUsers] = useState([]);

  useEffect(() => {
    fetchUsers().then(res => setUsers(res.data));
  }, []);

  return (
    <table className="table">
      <thead><tr><th>ID</th><th>Name</th><th>Age</th><th>Email</th><th>TimeStamp</th></tr></thead>
      <tbody>
        {users.map(u => (
          <tr key={u.id}>
            <td>{u.id}</td><td>{u.name}</td><td>{u.age}</td>
            <td>{u.email}</td><td>{new Date(u.timeStamp).toLocaleString()}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
