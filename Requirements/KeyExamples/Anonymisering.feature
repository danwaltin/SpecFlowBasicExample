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

Scenario: Personuppgifter anonymiseras för en av flera kunder
	Givet följande kunder
        | Förnamn | Efternamn | Epost            |
        | Ada     | Lovelace  | ada@example.com  |
        | Alan    | Turing    | alan@example.com |

    När kunden 'alan@example.com' anonymiseras

    Så ska följande kunder finnas
        | Förnamn      | Efternamn    | Epost           |
        | Ada          | Lovelace     | ada@example.com |
        | Anonymiserad | Anonymiserad |                 |
