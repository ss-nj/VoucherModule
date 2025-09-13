# VoucherModule

A modular .NET 8 Web API with clean architecture layers (Application / Infrastructure / Domain).  
Implements CRUD for `Person`, `Master`, and `Subsidiary` entities, plus reporting and Excel export.

---

## 🚧 TODOs
- [ ] Add full filtering for all inputs.
- [ ] Add unit/integration tests.
- [ ] Implement a full logging system.
- [ ] Implement soft delete for all entities.
- [ ] Move authentication settings (e.g. token expiration time) to configuration.

---

## 🧩 Hierarchy Implementation

For hierarchical data (Masters → Subsidiaries), I had three options:

1. **Nested Queries**  
2. **Join Table**  
3. **Path Field** (current approach)

I chose **path field** because:
- Although it’s not 3NF, it’s much simpler to implement.
- It enables one-step, fast lookups.
- Easier to read and update than deep joins.

A join table would provide more indexing options and potentially better performance but would have taken more time to implement.

---

## 🔐 Authentication

- Uses **JWT tokens** stored as **HttpOnly cookies**.
- Simple mock login:  
  **Username:** `admin`  
  **Password:** `1234`

---

## 📦 Features
- CRUD endpoints for `Person`, `Master`, `Subsidiary`.
- Excel export using **EPPlus**.
- Hierarchical reporting for debit/credit.

---

## 🛠️ Running Locally
```bash
dotnet build
dotnet run

.
