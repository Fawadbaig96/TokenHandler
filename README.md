# TokenUtility Class

The `TokenUtility` class, implemented within the `TokenHandler.Utilities` namespace, provides utility methods for working with tokens embedded in strings. It supports extracting tokens in the format `{{TokenName}}` and replacing them with values provided in a dictionary.

---

## Features

1. **Extract Tokens**: Identify and retrieve all tokens formatted as `{{TokenName}}` from a given string.
2. **Replace Tokens**: Dynamically replace tokens in a string with single or multiple values, with optional support for looping through templates using `loopSource`.

---

## Class Overview: `TokenUtility`

### Namespace
```csharp
namespace TokenHandler.Utilities;
```

### Implements
```csharp
ITokenHandler
```

The class adheres to the `ITokenHandler` interface, ensuring flexibility and easy integration.

---

### Methods

#### 1. `ExtractToken`
Extracts all tokens from a given string.

##### **Signature**
```csharp
public List<string> ExtractToken(string body)
```

##### **Parameters**
- **`body`**: The input string containing tokens to extract.

##### **Functionality**
- Uses a regex pattern `@"\{\{([^\}]+)\}\}"` to identify tokens enclosed in `{{ }}`.
- Returns a list of tokens, including the enclosing braces.

##### **Example**
```csharp
string body = "Hello {{FirstName}}, your balance is {{Balance}}.";
TokenUtility utility = new TokenUtility();
List<string> tokens = utility.ExtractToken(body);

// tokens = ["{{FirstName}}", "{{Balance}}"]
```

---

#### 2. `ReplaceTokens`
Replaces tokens in a string with their corresponding values from a dictionary.

##### **Signature**
```csharp
public string ReplaceTokens(string body, Dictionary<string, string> tokenValues, string? loopSource = null)
```

##### **Parameters**
- **`body`**: The string containing tokens to replace.
- **`tokenValues`**: A dictionary where:
  - Keys are tokens (e.g., `{{TokenName}}`).
  - Values are replacements (e.g., `"John"` for `{{FirstName}}`).
- **`loopSource`** *(optional)*: A JSON string representing a list of templates for handling tokens with multiple values.

##### **Functionality**
1. **Single Value Replacement**:
   - Replaces tokens with their respective single value from `tokenValues`.
   
2. **Multiple Value Replacement**:
   - Splits comma-separated values for tokens.
   - Uses the `loopSource` parameter to format replacements dynamically.
   - Appends the results for all values into a single string.

##### **Return Value**
A string with all tokens replaced by their respective values.

##### **Examples**
###### Single Token Replacement
```csharp
string body = "Welcome, {{UserName}}!";
var tokenValues = new Dictionary<string, string> { { "{{UserName}}", "Alice" } };

TokenUtility utility = new TokenUtility();
string result = utility.ReplaceTokens(body, tokenValues);

// Output: "Welcome, Alice!"
```

###### Multiple Token Values
```csharp
string body = "<table>{{RowTemplate}}</table>";
var tokenValues = new Dictionary<string, string>
{
    { "{{RowTemplate}}", "Row1,Row2,Row3" }
};

string loopSource = "[\"<tr>{{RowTemplate}}</tr>\"]";
TokenUtility utility = new TokenUtility();
string result = utility.ReplaceTokens(body, tokenValues, loopSource);

// Output: "<table><tr>Row1</tr><tr>Row2</tr><tr>Row3</tr></table>"
```

---

## Implementation Details

### Dependencies
- **`System.Text.Json`**:
  - Used for deserializing the `loopSource` JSON string.
- **`System.Text.RegularExpressions`**:
  - Used for regex matching of token patterns.

---

## Requirements
- **Language**: C#
- **Framework**: .NET Core or later.

---

## Notes
- Tokens must be enclosed in `{{ }}` to be recognized by the `ExtractToken` method.
- Ensure `loopSource` is a valid JSON-encoded string representing a list of templates for multiple-value replacements.
- Handles both single and multiple values seamlessly, with support for dynamic templates.

---

## License
The `TokenUtility` class is open for use in personal and professional projects. Contributions and enhancements are welcome.