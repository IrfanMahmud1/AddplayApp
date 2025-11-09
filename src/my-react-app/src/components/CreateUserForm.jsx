import { useState } from "react";
import { createUser } from "../api/userApi";

export default function CreateUserForm() {
  const [form, setForm] = useState({ name: "", age: "", email: "" });

  const handleChange = e =>
    setForm({ ...form, [e.target.name]: e.target.value });

  const handleSubmit = async e => {
    e.preventDefault();
    await createUser(form);
    alert("User created successfully!");
    setForm({ name: "", age: "", email: "" });
  };

  return (
    <form onSubmit={handleSubmit}>
      <input name="name" placeholder="Name" value={form.name} onChange={handleChange} />
      <input name="age" type="number" placeholder="Age" value={form.age} onChange={handleChange} />
      <input name="email" type="email" placeholder="Email" value={form.email} onChange={handleChange} />
      <button type="submit">Create</button>
    </form>
  );
}
