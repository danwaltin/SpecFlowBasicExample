Egenskap: Anonymisering

Scenario: Personuppgifter anonymiseras
	Givet en kund med följande uppgifter
        | Uppgift   | Värde           |
        | Förnamn   | Ada             |
        | Efternamn | Lovelace        |
        | Epost     | ada@example.com |

    När kunden anonymiseras

    Så ska kundens uppgifter vara
        | Uppgift   | Värde        |
        | Förnamn   | Anonymiserad |
        | Efternamn | Anonymiserad |
        | Epost     |              |
