# Average Lyric Count For Artists

This is my implementation of a console app that calculates the average number of lyrics for any given artist. 

To run the app, open the terminal, cd into the AireLogicTechTest folder and enter the following commands:

```bash
dotnet restore
dotnet build
dotnet run
```

---

### Testing

The project will use nunit to provide unit and integration tests.
Moq is used to mock repositories at boundaries and services in unit tests.
The integrations tests do not attempt to mock interaction with the console.

---

### Dependencies

The project is built in ASP.NET 6.

Further dependencies can be found in the ```package.json```

---

### Assumptions and potential improvements

This app makes use of 2 third party APIs

1. https://musicbrainz.org/doc/Development/XML_Web_Service/Version_2
2. https://lyricsovh.docs.apiary.io/#reference

Due to lack of time, certain assumptions will be made:

1. The result returned from music brainz with the highest score will be assumed to be the artist the user was searching for. I am also making the assumption that the top search result is the result with the highest score.
2. It makes the assumption that if the music brainz api were replaced wuth another, the new api would still work by searching for an artist to retrieve an ID that would be used in the search for songs titles


I contemplated allowing the user to interact further with the app, allowing them to select from the list of artists returned (or the top x).

Given more time, it would be worth applying a cacheing layer and throttling to the calls to the third party APIs to avoid hitting their rate limits. 



