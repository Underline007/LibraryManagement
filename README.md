# Library Management

A web application for managing books in a library, allowing users to borrow books, control the borrowing limits, and notify users about the status of their borrowed books via email.

## Project Information

- **Duration**: May 2024 - June 2024
- **Team Size**: 1
- **Position**: Full-stack Developer

## Description

This project provides functionalities for a library management system, including:

- Allowing users to borrow books from the library.
- Controlling the user's borrowing of books by limiting the number of times books can be borrowed per month and the number of books per loan.
- Notifying users about the status of their borrowed books through email.

## Technologies Used

- **Backend**: .NET API 8, SQL Server
- **Frontend**: ReactJS, Ant Design, Tailwind CSS
- **Other**: LinQ, Cloudinary, MailKit

## Features

- **Book Borrowing**: Users can borrow books, and the system tracks the borrowing limit and the number of books.
- **Email Notifications**: Users receive email notifications about the status of their borrowed books.
- **Image Management**: Cloudinary is used for managing book cover images.
- **Responsive Design**: The application uses Tailwind CSS for responsive design.

## Setup and Installation

   1. git clone https://github.com/Underline007/LibraryManagement.git
   2. set up Cloudinary, SQL, and MailService in appsetting.json
   3. migration project
   4. cd backend -> dotnet run
   5. cd front-end -> npm i -> npm start



