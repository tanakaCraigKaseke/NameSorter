# Name Sorter

Console application that sorts names by **last name**, then by **given names** (1–3 allowed).

## Requirements
- .NET 8 SDK

## Build & Run

```bash
# From the repo root
dotnet build

# Run with sample input
dotnet run --project ./src/NameSorter ./src/NameSorter/unsorted-names-list.txt

# Output will print to console and write to ./sorted-names-list.txt
```

## Tests
```bash
dotnet test
```

## Design

- **SOLID**: Reader/Writer/Sorter via abstractions (`INameReader`, `INameWriter`, `INameSorter`).
- **Empathy**: Small focused classes, clear naming, guard clauses.
- **Deterministic sort**: Ordinal string comparison for predictability.

## Notes
- Names must have at least **two tokens** (>= 1 given name + last name) and at most **four** (3 given + last).
- Blank lines are ignored.
# NameSorter
