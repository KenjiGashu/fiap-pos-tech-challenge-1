# Code analysis
## fiap-pos 
#### Branch main
#### Version not provided 

**By: default**

*Date: 2026-05-08*

*Analyzed the: 2026-05-08*

## Introduction
This document contains results of the code analysis of fiap-pos



## Configuration

- Quality Profiles
    - Names: Sonar way [C#]; Sonar way [CSS]; Sonar way [Docker]; Sonar way [JavaScript]; Sonar way [HTML]; Sonar way [XML]; 
    - Files: ab301ab5-1e96-4818-9e33-d5b83157c8f5.json; f6b70d52-edf1-4c4b-9650-58897284a329.json; db2e079d-9e8e-419c-9b83-2e3cf4dc4530.json; 16b9ed8b-ed7d-4f18-8a83-dc57afa299b0.json; 56479b65-4126-494a-ae81-b4023c873d63.json; 592425a0-2dc8-42ac-a3fd-a84f48cdc01a.json; 


 - Quality Gate
    - Name: Sonar way
    - File: Sonar way.xml

## Synthesis

### Analysis Status

Reliability | Security | Security Review | Maintainability |
:---:|:---:|:---:|:---:
A | A | E | A |

### Quality gate status

| Quality Gate Status | OK |
|-|-|



### Metrics

Coverage | Duplications | Comment density | Median number of lines of code per file | Adherence to coding standard |
:---:|:---:|:---:|:---:|:---:
0.0 % | 1.9 % | 11.3 % | 38.0 | 99.3 %

### Tests

Total | Success Rate | Skipped | Errors | Failures |
:---:|:---:|:---:|:---:|:---:
0 | 0 % | 0 | 0 | 0

### Detailed technical debt

Reliability|Security|Maintainability|Total
---|---|---|---
-|-|1d 4h 11min|1d 4h 11min


### Metrics Range

\ | Cyclomatic Complexity | Cognitive Complexity | Lines of code per file | Coverage | Comment density (%) | Duplication (%)
:---|:---:|:---:|:---:|:---:|:---:|:---:
Min | 0.0 | 0.0 | 4.0 | 0.0 | 0.0 | 0.0
Max | 729.0 | 109.0 | 3061.0 | 0.0 | 45.1 | 16.6

### Volume

Language|Number
---|---
C#|3037
Docker|47
Total|3084


## Issues

### Issues count by severity and types

Type / Severity|INFO|MINOR|MAJOR|CRITICAL|BLOCKER
---|---|---|---|---|---
BUG|0|0|0|0|0
VULNERABILITY|0|0|0|0|0
CODE_SMELL|22|5|104|0|0


### Issues List

Name|Description|Type|Severity|Number
---|---|---|---|---
General or reserved exceptions should never be thrown||CODE_SMELL|MAJOR|34
Unused private types or members should be removed||CODE_SMELL|MAJOR|3
Sections of code should not be commented out||CODE_SMELL|MAJOR|2
Fields that are only assigned in the constructor should be "readonly"||CODE_SMELL|MAJOR|1
Exceptions should not be explicitly rethrown||CODE_SMELL|MAJOR|1
Awaitable method should be used||CODE_SMELL|MAJOR|1
Fields should not have public accessibility||CODE_SMELL|MINOR|1
Methods and properties that don't access instance data should be static||CODE_SMELL|MINOR|3
Method overloads should be grouped together||CODE_SMELL|MINOR|1
external_roslyn:CS8618|Non-nullable property 'Message' must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring the property as nullable.|CODE_SMELL|MAJOR|13
external_roslyn:CA1850|Prefer static 'System.Security.Cryptography.SHA256.HashData' method over 'ComputeHash'|CODE_SMELL|INFO|1
external_roslyn:CA2200|Re-throwing caught exception changes stack information|CODE_SMELL|MAJOR|1
external_roslyn:CA1827|Count() is used where Any() could be used instead to improve performance|CODE_SMELL|INFO|14
external_roslyn:CA1859|Change type of field 'service' from 'Gashu.SistemaMecanica.Application.OrdensServico.Interfaces.IOrdemServicoService' to 'Gashu.SistemaMecanica.Application.OrdensServico.Services.OrdemServicoService' for improved performance|CODE_SMELL|INFO|3
external_roslyn:CS1591|Missing XML comment for publicly visible type or member 'ConfirmacaoEmailController.ConfirmacaoEmailController(INotificacaoService)'|CODE_SMELL|MAJOR|8
external_roslyn:CA1822|Member 'HashPassword' does not access instance data and can be marked as static|CODE_SMELL|INFO|4
external_roslyn:CS8603|Possible null reference return.|CODE_SMELL|MAJOR|3
external_roslyn:CS8604|Possible null reference argument for parameter 'source' in 'PecaResponseDto? Enumerable.FirstOrDefault(IEnumerable source)'.|CODE_SMELL|MAJOR|5
external_roslyn:CS8601|Possible null reference assignment.|CODE_SMELL|MAJOR|3
external_roslyn:CS8602|Dereference of a possibly null reference.|CODE_SMELL|MAJOR|28
external_roslyn:CS8613|Nullability of reference types in return type of 'Task ServicoRepository.ObterPorId(Guid id)' doesn't match implicitly implemented member 'Task IServicoRepository.ObterPorId(Guid id)'.|CODE_SMELL|MAJOR|1


## Security Hotspots

### Security hotspots count by category and priority

Category / Priority|LOW|MEDIUM|HIGH
---|---|---|---
LDAP Injection|0|0|0
Object Injection|0|0|0
Server-Side Request Forgery (SSRF)|0|0|0
XML External Entity (XXE)|0|0|0
Insecure Configuration|0|0|0
XPath Injection|0|0|0
Authentication|0|0|0
Weak Cryptography|0|0|0
Denial of Service (DoS)|0|0|0
Log Injection|0|0|0
Cross-Site Request Forgery (CSRF)|0|0|0
Open Redirect|0|0|0
Permission|0|4|0
SQL Injection|0|0|0
Encryption of Sensitive Data|0|0|0
Traceability|0|0|0
Buffer Overflow|0|0|0
File Manipulation|0|0|0
Code Injection (RCE)|0|0|0
Cross-Site Scripting (XSS)|0|0|0
Command Injection|0|0|0
Path Traversal Injection|0|0|0
HTTP Response Splitting|0|0|0
Others|0|0|0


### Security hotspots

Category|Name|Priority|Severity|Count
---|---|---|---|---
Permission|Recursively copying context directories is security-sensitive|MEDIUM|CRITICAL|2
Permission|Running containers as a privileged user is security-sensitive|MEDIUM|MINOR|2

