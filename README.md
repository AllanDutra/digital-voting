
## 🗳️ Digital Voting

The project developed is a digital vote in which the voter can vote in several ballots but only in one option per vote.

For example, given the poll with description "What's your favorite color?" and the voting options: "Blue", "Red" and "Green". The voter can only vote for one of these 3 options. When voting, the vote is counted for the overall vote count and for the count of the chosen voting option.

No "position" restrictions were required for the existing methods, just the authentication restriction so that the system can keep track of the people who have already voted.

The following technologies/patterns were used in this project:
- C#;
- ASP .NET Core;
- .NET 7.0;
- Entity Framework Core;
- Dapper;
- PostgreSQL;
- JWT authentication;
- Fluent Validation;
- CQRS;
- Repository Pattern;

## 📈 Diagrams

### Use Case

<img src="https://ik.imagekit.io/ghmg33v8b/projects/digital-voting/use-case_-k3H-q9SRw.png?ik-sdk-version=javascript-1.4.3&updatedAt=1678145374581" alt="use-case" />

### ERD — Entity Relationship Diagram

<img src="https://ik.imagekit.io/ghmg33v8b/projects/digital-voting/erd_PZVKq5GS_0.png?ik-sdk-version=javascript-1.4.3&updatedAt=1678145836100" alt="erd" />

## 📫  Routes

### Voter Controller

<img src="https://img.shields.io/badge/-POST-%2349CC90" height="30" />

**"/api/voter/sign-up"**

_Registers a new voter with his password and unique username_

**body:**

```
{
     "username": string,
     "password": string
}
```

**response:**
```
{
     "message": string
}
```

<hr>

<img src="https://img.shields.io/badge/-POST-%2349CC90" height="30" />

**"/api/voter/sign-in"**

_Generates a new access token to voter_

**body:**

```
{
     "username": string,
     "password": string
}
```

**response:**
```
{
     "token": string
}
```

<hr>

<img src="https://img.shields.io/badge/-POST-%2349CC90" height="30" />

**"/api/voter/vote"**

_Registers a new vote (Each user can vote only once in each poll)._

**query params:**

`VotingOption_Id: Guid`

**response:**
```
{
     "message": string
}
```

<hr>

### Poll Controller

<img src="https://img.shields.io/badge/-POST-%2349CC90" height="30" />

**"/api/poll/create"**

_Create a new poll with yours voting options_

**required headers:**

`Authorization: Bearer {token JWT}`

**body:**

```
{
     "pollDescription": string,
     "pollVotingOptions": List<string>
}
```

**response:**
```
{
     "message": string,
     "pollId": Guid
}
```

<hr>

<img src="https://img.shields.io/badge/-POST-%2349CC90" height="30" />

**"/api/poll/create-option"**

_Creates a new poll option for an existing poll._

**required headers:**

`Authorization: Bearer {token JWT}`

**body:**

```
{
     "pollId": Guid,
     "description": string
}
```


**response:**
```
{
     "message": string,
     "votingOptionId": Guid
}
```

<hr>

<img src="https://img.shields.io/badge/-GET-%2361AFFE" height="30" />

**"/api/poll/get-all"**

_Gets all polls with all their voting options_

**response:**
```
[
     {
          "id": Guid,
          "description": string,
          "amountOfVotes": int,
          "votingOptions": [
               {
                    "id": Guid,
                    "description": string,
                    "amountOfVotes": int
               }
          ]
     }
]
```

<hr>

<img src="https://img.shields.io/badge/-DELETE-%23F93E3E" height="30" />

**"/api/poll/delete-option/{optionId}"**

_Delete a specific option by its id_

**required headers:**

`Authorization: Bearer {token JWT}`

**route params:**

`optionId: Guid`


**response:**
```
{
     "message": string
}
```

<hr>

<img src="https://img.shields.io/badge/-DELETE-%23F93E3E" height="30" />

**"/api/poll/delete/{pollId}"**

_Deletes a poll and all of its options_

**required headers:**

`Authorization: Bearer {token JWT}`

**route params:**

`pollId: Guid`

**response:**
```
{
     "message": string
}
```


## 🌐 Status
<p>Finished project ✅</p>

## 🧰 Prerequisites

- .NET 7.0 or +

- Connection string to Postgre SQL BD in **digital-voting/DigitalVoting.API/appsettings.json** named as ConnectionStrings.DigitalVotingCs

- Secret key to be symmetric key of JWT encryption in **digital-voting/DigitalVoting.API/appsettings.json** named as SecretKey

## <img src="https://icon-library.com/images/database-icon-png/database-icon-png-13.jpg" width="20" /> Database

_Run the following SCRIPT in the PostgreSQL query tool:_

```
BEGIN;


CREATE TABLE IF NOT EXISTS public."Poll"
(
    "Id" uuid NOT NULL,
    "Description" character varying COLLATE pg_catalog."default" NOT NULL,
    "AmountOfVotes" integer NOT NULL,
    CONSTRAINT "Poll_pkey" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS public."Voter"
(
    "Username" character varying COLLATE pg_catalog."default" NOT NULL,
    "Password" character varying COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Voter_pkey" PRIMARY KEY ("Username")
);

CREATE TABLE IF NOT EXISTS public."Voter_VotingOption"
(
    "Id" uuid NOT NULL,
    "Voter_Username" character varying COLLATE pg_catalog."default" NOT NULL,
    "VotingOption_Id" uuid NOT NULL,
    "Poll_Id" uuid NOT NULL,
    CONSTRAINT "Voter_VotingOption_pkey" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS public."VotingOption"
(
    "Id" uuid NOT NULL,
    "Description" character varying COLLATE pg_catalog."default" NOT NULL,
    "AmountOfVotes" integer,
    "Poll_Id" uuid NOT NULL,
    CONSTRAINT "VotingOption_pkey" PRIMARY KEY ("Id")
);

ALTER TABLE IF EXISTS public."Voter_VotingOption"
    ADD CONSTRAINT "Voter_VotingOption_Voter_Username_fkey" FOREIGN KEY ("Voter_Username")
    REFERENCES public."Voter" ("Username") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION
    NOT VALID;


ALTER TABLE IF EXISTS public."Voter_VotingOption"
    ADD CONSTRAINT "Voter_VotingOption_VotingOption_Id_fkey" FOREIGN KEY ("VotingOption_Id")
    REFERENCES public."VotingOption" ("Id") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION
    NOT VALID;


ALTER TABLE IF EXISTS public."VotingOption"
    ADD CONSTRAINT "VotingOption_Poll_Id_fkey" FOREIGN KEY ("Poll_Id")
    REFERENCES public."Poll" ("Id") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION
    NOT VALID;

END;
```

## 🔧 Installation
`$ git clone https://github.com/AllanDutra/digital-voting.git`

`$ cd digital-voting/DigitalVoting.API`

`$ dotnet restore`

`$ dotnet run`

**Server listenning at  [https://localhost:5001/](https://localhost:5001/)!**

## 🔨 Tools used

<div>
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg" width="80" /> 
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg" width="80" />
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/postgresql/postgresql-original-wordmark.svg" width="80" />
</div>
