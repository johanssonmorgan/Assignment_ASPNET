INSERT INTO [Status] (StatusName)
VALUES ('Active');

INSERT INTO [Clients] (Id, ClientName)
VALUES ('7a63dc80-3156-4d0c-bf92-4e2a0ddc2a9a', 'GitLab Inc.');

INSERT INTO [AspNetUsers] (
    Id, 
    FirstName, 
    LastName, 
    EmailConfirmed, 
    PhoneNumberConfirmed, 
    TwoFactorEnabled, 
    LockoutEnabled, 
    AccessFailedCount
)
VALUES (
    '1c0d89ba-75d7-419e-aaf6-ffe438c108d6',
    'John',
    'Doe',
    0,
    0,
    0,
    0,
    0
);

INSERT INTO [Projects] (
    Id, 
    ProjectName, 
    Description, 
    StartDate, 
    Budget, 
    Created, 
    UserId, 
    ClientId, 
    StatusId
)
VALUES (
    '3f2504e0-4f89-11d3-9a0c-0305e82c3301',
    'Website Redesign',
    'It is necessary to develop a website redesign in a corporate style.',
    GETDATE(),
    '1999',
    GETDATE(),
    '1c0d89ba-75d7-419e-aaf6-ffe438c108d6',
    '7a63dc80-3156-4d0c-bf92-4e2a0ddc2a9a',
    '1'
);