# TermSearcher

Bibliothèque .NET permettant à un utilisateur d'obtenir des suggestions de termes selon un terme donné, selon une liste de choix.

## Description

TermSearcher cherche selon un terme donné Td les termes dans une liste de choix Tc étant les plus proche de td, en comparant la chaîne de caractère composant Td avec chaque éléments de Tc. Les éléments de Tc ayant le moins de différences (égale au nombre de caractères) sont retournées, selon un nombre de termes suggérés voulus  par l'utilisateur.
Les critères de longueurs et similarité sont pris en compte.

## Fonctionnalités

- Recherche de termes similaires basée sur un score de différence
- Support des sous-chaînes pour les recherches partielles
- Tri des résultats par score de similarité, longueur et ordre lexicographique
- Gestion insensible à la casse

## Prérequis

- .NET 8.0

## Installation

```bash
dotnet build
```

## Utilisation

```csharp
using TermSearcherAppCnsle;

var searcher = new TermSearcher();
var choices = new List<string> { "apple", "application", "apply", "banana", "orange" };
var suggestions = searcher.GetSuggestions("app", choices, 3);

foreach (var suggestion in suggestions)
{
    Console.WriteLine(suggestion);
}
```

## Tests

Le projet inclut une suite de tests unitaires utilisant NUnit.

Pour exécuter les tests :

```bash
dotnet test
```

Pour exécuter les tests avec couverture de code :

```bash
dotnet test --collect:"XPlat Code Coverage"
```

## Couverture de code

Les rapports de couverture de code sont disponibles dans le dossier `coveragereport/`.

## Structure du projet

- `TermSearcher/` - Bibliothèque principale
  - `ITermSearcher.cs` - Interface du moteur de recherche
  - `TermSearcher.cs` - Implémentation du moteur de recherche
- `TermSearcher.Tests/` - Tests unitaires
- `coveragereport/` - Rapports de couverture de code
