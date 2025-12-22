## Extensions Exercises

### Exercise 1: String Manipulation Extensions
Create useful string extension methods following functional programming principles.

**Tasks:**
1. Create extension methods for `string`:
   - `Truncate(int maxLength, string suffix = "...")` - truncate with ellipsis
   - `ToTitleCase()` - capitalize first letter of each word

**Expected concepts:**
- Extension method syntax (`this` parameter)
- Method chaining

---

### Exercise 2: LINQ-Style Collection Extensions
Implement custom LINQ-style extension methods for `IEnumerable<T>`.

**Tasks:**
1. Create extension methods:
   - `IEnumerable<T> TakeEveryNth<T>(this IEnumerable<T> source, int n)`

**Expected concepts:**
- Generic extension methods
- Deferred execution with `yield return`
- `IEnumerable<T>` manipulation

---

