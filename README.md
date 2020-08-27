<space><space>

# Sparta Profiles Viewer

- [Sparta Profiles Viewer](#sparta-profiles-viewer)
  - [Project Description](#project-description)
  - [Timeline](#timeline)
  - [Product](#product)
    - [Functional Requirements](#functional-requirements)
    - [Non functional Requirements](#non-functional-requirements)
    - [Types of Users](#types-of-users)
    - [Profile sections](#profile-sections)
    - [Developer environment](#developer-environment)
      - [Backend](#backend)
      - [Frontend](#frontend)
  - [ERD](#erd)
  - [Conclusion](#conclusion)
    - [Project - Definition of Done](#project---definition-of-done)
    - [User stroy - Definition of Done](#user-stroy---definition-of-done)
    - [Gantt Chart???](#gantt-chart)
  - [Sprint Layouts](#sprint-layouts)
    - [Sprint 0 - Planning](#sprint-0---planning)
      - [Overview:](#overview)
      - [Sprint Review:](#sprint-review)
      - [Sprint Retrospective](#sprint-retrospective)
      - [What was done:](#what-was-done)
    - [Sprint 1 - MVP](#sprint-1---mvp)
    - [Sprint 2 - Permissions implementation](#sprint-2---permissions-implementation)
    - [Sprint 3 - ...](#sprint-3---)
    - [Sprint 4 - ...](#sprint-4---)
  - [MVP](#mvp)
    - [MVP Description](#mvp-description)

---

## Project Description

Currently at Sparta Global the resource management team are sending out emails to prospective clients with a PDF attachment of the Spartans they believe would suit the potential client. The issue with this was that, full form PDF documents with Spartan information are too much for clients to read. Worst case scenario, the emails with the PDFs attached do not even get past the potential clients email security. All in all, the efforts of the resource team are wasted.

In order to combat this, the resource management team having been sending out brief PDF documents to clients with just the important details. This has shown an increase in the responses recieved from these email campaigns. Sparta Global would like to continue this, and send out brief PDF documents however there is still the issue of getting past potential clients email security.

Sparta Global would like create a portal system which would display all Spartan profiles. This would be web based which the Resource teams can use to send url links to Spartan profiles as opposed to attaching PDF documents.

---

## Timeline
The proof of concept project has a deadline of 2 weeks to be completed, from planning to a working project.

---

## Product - Key Details

### Functional Requirements
* Display Spartan Profiles
* Filter Spartan Profiles by academy stream

### Non functional Requirements
* The page should be easy to navigate

### Types of Users
* Admin
* Resource manager
* Client
* Spartan Student

### Profile sections
* Name
* Summary
* Stream
* Approval Status (Accepted, Needs Edit, ...)
* Job Status (Training, Onsite, ...)
* Education
* Employment
* Projects
* Hobbies

### Developer environment

#### Backend
* Azure database
* ASP .NET core Model and Controller

#### Frontend
* Razor pages View
* JS
* CSS
* HTML

---

## ERD
This Entity Relationship Diagram shows a blueprint of the database used for the project and the relationships between the tables.
![ERD for database](images/database-erd.png)

---

## Conclusion

### Description

An admin can register other users. If a user is a student can create, view and edit their own profile page. If a user is a resource manager they can approve and disapprove a profile, if is approved their profile is live. If a user is a client, can be registered by admin, can search with a filter and view Sparta profiles and export them.

### Definition of Done - Project

* Completed code uploaded to GitHub and merged into master branch
* README file provides thorough documentation of the application
* Code is concise and clear, following good naming conventions Application encapsulated behind a well designed and easy to use user interface.

### Definition of Done - User Stories
* Code build with no error
* Code review is complete
* Acceptance criteria are met
* The user story is both implemented in Model and GUI
* 80% of unit test passing

---

## Sprint Layouts 

### Sprint 0 - Planning

#### Overview:

* Wednesday 26th - Thursday 27th 
* Scrum Master: **Chen** 
* Task: Project planning and proposing

#### Sprint Review:
- What was accomplished ...

#### Sprint Retrospective
- What went well, what could be improve ...

#### What was done:

Wednesday 
- 9:30 - 16:00 - Meeting with Emer and initial plans

Thursday
- 9:30 - 11:00 - Created acceptance criteria
- 11:00 - 11:15 - Creating project document
- 11:15 - 11:30 - Break
- 11:30 - 12:20 - Call with Astha and improved acceptance criteria
- 12:20 - 12:30 - Review of call with Astha
- 12:30 - 13:30 - Lunch
- 13:30 - 13:40 - Created Github and added team members as collaborators
- 13:40 - 14:30 - Acceptance criteria updated to follow Gherkin Cucumber syntax

Notes: 
- Summary due monday and CV
- Plan revision day
- Make Github
  
---

### Sprint 1 - Project setup and MVP

* Friday 28th - Wednesday 2nd 
* Scrum Master: **Alex** 
* Task: Creating and MVP product where a user can create and view a profile

* **Review Conclusion: ... ??** with Richard and Emer
* **Retro Conclusion: ... ??** with Whole Team

---

### Sprint 2 - Permissions implementation 

* Wednesday 28th - Friday 4th 
* Scrum Master: **Bruno** 
* Task: Creating Login, Viewing based on permissions

* **Review Conclusion: ... ??** with Richard and Emer
* **Retro Conclusion: ... ??** with Whole Team

---

### Sprint 3 - Exporting, Admin Panel, Viewing

* Monday 7th - Wednesday 9th 
* Scrum Master: **Bryn** 
* Task: Profile displays, Exporting, Client viewing in teams format

* **Review Conclusion: ... ??** with Richard and Emer
* **Retro Conclusion: ... ??** with Whole Team

---

### Sprint 4 - Clean up
* Thursday 10th - Friday 11th 
* Scrum Master: **Harry** 
* Task: Polishing, Testings and Presentation

* **Review Conclusion: ... ??** with Richard and Emer
* **Retro Conclusion: ... ??** with Whole Team

---

## MVP

### MVP Description
* Creating a page with input boxes to create a online Sparta Profile and view all the profiles based on the layout given by product owner
