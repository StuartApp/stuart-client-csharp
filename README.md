# Stuart C# Client
Created and provided by [OrderYOYO](https://orderyoyo.com)

For a complete documentation of all endpoints offered by the Stuart API, you can visit [Stuart API documentation](https://stuart.api-docs.io).

## Install
Via NuGet Package Manager:

``` bash
$ PM> Install-Package stuart-client-csharp -Version 1.3.1
```

## Usage

1. [Initialize Client](#initialize-client)
2. [Create a Job](#create-a-job)
    1. [Minimalist](#minimalist)
    2. [Complete](#complete)
        1. [With scheduling at pickup](#with-scheduling-at-pickup)
        1. [With scheduling at drop off](#with-scheduling-at-dropoff)
        2. [With stacking (multi-drop)](#with-stacking-multi-drop)
3. [Get a Job](#get-a-job)
4. [Cancel a Job](#cancel-a-job)
5. [Validate a Job](#validate-a-job)
6. [Cancel a delivery](#cancel-a-delivery)
7. [Get a pricing](#get-a-pricing)
8. [Get a job eta to pickup](#get-a-job-eta-to-pickup)

### Initialize client

```c#
var clientId = "<put_your_ID_here>"; // can be found here: https://admin-sandbox.stuart.com/client/api
var clientSecret = "<put_your_secret_here>"; // can be found here: https://admin-sandbox.stuart.com/client/api
var environment = Environment.Sandbox;
var stuartApi = StuartApi.Initialize(environment, clientId, clientSecret);
```

### Create a Job

**Important**: Even if you can create a Job with a minimal set of parameters, we **highly recommend** that you fill as many information as 
you can in order to ensure the delivery process goes well.

#### Minimalist

##### Package size based
```c#
var job = new JobRequest
{
    Job = new Job
    {
        PickUps = new List<PickUp>
        {
            new PickUp
            {
                Address = "12 rue rivoli, 75001 Paris"
            }
        },
        DropOffs = new List<DropOff>
        {
            new DropOff
            {
                Address = "42 rue rivoli, 75001 Paris",
                PackageType = PackageSizeType.small
            }
        }
    }
};

var createdJob = await StuartApi.Job.CreateJob(job);
```

##### Transport type based (France only)
```c#
var job = new JobRequest
{
    Job = new Job
    {
        TransportType = TransportType.bike
        PickUps = new List<PickUp>
        {
            new PickUp
            {
                Address = "12 rue rivoli, 75001 Paris"
            }
        },
        DropOffs = new List<DropOff>
        {
            new DropOff
            {
                Address = "42 rue rivoli, 75001 Paris"
            }
        }
    }
};

var createdJob = await StuartApi.Job.CreateJob(job);
```

#### Complete

##### Package size based

```c#
var job = new JobRequest
{
    Job = new Job
    {
        PickUps = new List<PickUp>
        {
            new PickUp
            {
                Address = "12 rue rivoli, 75001 Paris",
                Comment = "Wait outside for an employee to come.",
                Contact = new Contact
                {
                    FirstName = "Martin",
                    LastName = "Pont",
                    Phone = "+33698348756",
                    Company = "KFC Paris Barbès"
                }
            }
        },
        DropOffs = new List<DropOff>
        {
            new DropOff
            {
                PackageType = PackageSizeType.small,
                PackageDescription = "Pizza box",
                ClientReference = "12345678ABCDE", // Must be unique
                Address = "42 rue rivoli, 75001 Paris",
                Comment = "code: 3492B. 3e étage droite. Sonner à Durand.",
                Contact = new Contact
                {
                    FirstName = "Alex",
                    LastName = "Durand",
                    Phone = "+33634981209",
                }
            }
        }
    }
};

var createdJob = await StuartApi.Job.CreateJob(job);
```

##### Transport type based (France only)

```c#
var job = new JobRequest
{
    Job = new Job
    {
        TransportType = TransportType.bike
        PickUps = new List<PickUp>
        {
            new PickUp
            {
                Address = "12 rue rivoli, 75001 Paris",
                Comment = "Wait outside for an employee to come.",
                Contact = new Contact
                {
                    FirstName = "Martin",
                    LastName = "Pont",
                    Phone = "+33698348756",
                    Company = "KFC Paris Barbès"
                }
            }
        },
        DropOffs = new List<DropOff>
        {
            new DropOff
            {
                PackageDescription = "Pizza box",
                ClientReference = "12345678ABCDE", // Must be unique
                Address = "42 rue rivoli, 75001 Paris",
                Comment = "code: 3492B. 3e étage droite. Sonner à Durand.",
                Contact = new Contact
                {
                    FirstName = "Alex",
                    LastName = "Durand",
                    Phone = "+33634981209",
                }
            }
        }
    }
};

var createdJob = await StuartApi.Job.CreateJob(job);
```

#### With scheduling at pickup

For more information about job scheduling you should [check our API documentation](https://stuart.api-docs.io/v2/jobs/scheduling-a-job).

```c#
var job = new JobRequest
{
    Job = new Job
    {
        PickUpAt = DateTime.UtcNow.AddHours(2),
        PickUps = new List<PickUp>
        {
            new PickUp
            {
                Address = "12 rue rivoli, 75001 Paris"
            }
        },
        DropOffs = new List<DropOff>
        {
            new DropOff
            {
                Address = "42 rue rivoli, 75001 Paris",
                PackageType = PackageSizeType.small
            }
        }
    }
};

var createdJob = await StuartApi.Job.CreateJob(job);
```

#### With scheduling at dropoff

For more information about job scheduling you should [check our API documentation](https://stuart.api-docs.io/v2/jobs/scheduling-a-job).

Please note that this feature can only be used with only one dropoff.

```c#
var job = new JobRequest
{
    Job = new Job
    {
        DropOffAt = DateTime.UtcNow.AddHours(2),
        PickUps = new List<PickUp>
        {
            new PickUp
            {
                Address = "12 rue rivoli, 75001 Paris"
            }
        },
        DropOffs = new List<DropOff>
        {
            new DropOff
            {
                Address = "42 rue rivoli, 75001 Paris",
                PackageType = PackageSizeType.small
            }
        }
    }
};

var createdJob = await StuartApi.Job.CreateJob(job);
```

#### With stacking (multi-drop)

##### Package size based

```c#
var job = new JobRequest
{
    Job = new Job
    {
        PickUps = new List<PickUp>
        {
            new PickUp
            {
                Address = "12 rue rivoli, 75001 Paris",
                Comment = "Wait outside for an employee to come.",
                Contact = new Contact
                {
                    FirstName = "Martin",
                    LastName = "Pont",
                    Phone = "+33698348756",
                    Company = "KFC Paris Barbès"
                }
            }
        },
        DropOffs = new List<DropOff>
        {
            new DropOff
            {
                PackageType = PackageSizeType.small,
                PackageDescription = "Red packet.",
                ClientReference = "12345678ABCDE", // Must be unique
                Address = "42 rue rivoli, 75001 Paris",
                Comment = "code: 3492B. 3e étage droite. Sonner à Durand.",
                Contact = new Contact
                {
                    FirstName = "Alex",
                    LastName = "Durand",
                    Phone = "+33634981209",
                }
            },
            new DropOff
            {
                PackageType = PackageSizeType.small,
                PackageDescription = "Blue packet.",
                ClientReference = "ABCDE213124", // Must be unique
                Address = "156 rue de Charonne, 75011 Paris",
                Comment = "code: 92A42. 2e étage gauche",
                Contact = new Contact
                {
                    FirstName = "Maximilien",
                    LastName = "Lebluc",
                    Phone = "+33632341209",
                }
            }
        }
    }
};

var createdJob = await StuartApi.Job.CreateJob(job);
```

##### Transport type based (France only)

```c#
var job = new JobRequest
{
    Job = new Job
    {
        TransportType = TransportType.bike
        PickUps = new List<PickUp>
        {
            new PickUp
            {
                Address = "12 rue rivoli, 75001 Paris",
                Comment = "Wait outside for an employee to come.",
                Contact = new Contact
                {
                    FirstName = "Martin",
                    LastName = "Pont",
                    Phone = "+33698348756",
                    Company = "KFC Paris Barbès"
                }
            }
        },
        DropOffs = new List<DropOff>
        {
            new DropOff
            {
                PackageDescription = "Red packet.",
                ClientReference = "12345678ABCDE", // Must be unique
                Address = "42 rue rivoli, 75001 Paris",
                Comment = "code: 3492B. 3e étage droite. Sonner à Durand.",
                Contact = new Contact
                {
                    FirstName = "Alex",
                    LastName = "Durand",
                    Phone = "+33634981209",
                }
            },
            new DropOff
            {
                PackageDescription = "Blue packet.",
                ClientReference = "ABCDE213124", // Must be unique
                Address = "156 rue de Charonne, 75011 Paris",
                Comment = "code: 92A42. 2e étage gauche",
                Contact = new Contact
                {
                    FirstName = "Maximilien",
                    LastName = "Lebluc",
                    Phone = "+33632341209",
                }
            }
        }
    }
};

var createdJob = await StuartApi.Job.CreateJob(job);
```

### Get a Job

Once you successfully created a Job you can retrieve it this way:

```c#
var jobId = 126532;
var job = await StuartApi.Job.GetJob(jobId);
```

Or when you create a new Job:

```c#
var job = new JobRequest
{
    Job = new Job
    {
        PickUps = new List<PickUp>
        {
            new PickUp
            {
                Address = "12 rue rivoli, 75001 Paris"
            }
        },
        DropOffs = new List<DropOff>
        {
            new DropOff
            {
                Address = "42 rue rivoli, 75001 Paris",
                PackageType = PackageSizeType.small
            }
        }
    }
};

var createdJob = await StuartApi.Job.CreateJob(job);    
createdJob.Deliveries
```

The Stuart API determine the optimal route on your behalf, 
that's why the `createdJob.Deliveries` propery will be empty 
`List` when the Job has not been created yet. The `createdJob.Deliveries` 
propery will contain a `List` of `Delivery` as soon as the Job is created.

### Cancel a job

Once you successfully created a Job you can cancel it in this way:

```c#
var jobId = 126532;
await StuartApi.Job.CancelJob(jobId);
```

The method `CancelJob();` will return only `Task` if the job was cancelled. If
there was an error, it will throw an exception with information about reason.

For more details about how cancellation works, please refer to our [dedicated documentation section](https://stuart.api-docs.io/v2/jobs/job-cancellation).

### Validate a Job

Before creating a Job you can validate it (control delivery area & address format). Validating a Job is **optional** and does not prevent you from creating a Job.

```c#
var job = new JobRequest
{
    Job = new Job
    {
        PickUps = new List<PickUp>
        {
            new PickUp
            {
                Address = "12 rue rivoli, 75001 Paris"
            }
        },
        DropOffs = new List<DropOff>
        {
            new DropOff
            {
                Address = "42 rue rivoli, 75001 Paris",
                PackageType = PackageSizeType.small
            }
        }
    }
};

var result = await StuartApi.Job.ValidateParameters(job);
```

The result will hold the boolean value `true` if the job is valid. If
there was an error, it will throw an exception with information about reason.

### Cancel a delivery

Once you successfully created a Delivery you can cancel it in this way:

```c#
var deliveryId = 126532;
await StuartApi.Job.CancelDelivery(deliveryId);
```


### Get a pricing

Before creating a Job you can ask for a pricing. Asking for a pricing is **optional** and does not prevent you from creating a Job.

```c#
var job = new JobRequest
{
    Job = new Job
    {
        PickUps = new List<PickUp>
        {
            new PickUp
            {
                Address = "12 rue rivoli, 75001 Paris"
            }
        },
        DropOffs = new List<DropOff>
        {
            new DropOff
            {
                Address = "42 rue rivoli, 75001 Paris",
                PackageType = PackageSizeType.small
            }
        }
    }
};

var result = await StuartApi.Job.RequestJobPricing(job);    
    
result.Amount; // example: 11.5
result.Currency; // example: "EUR"
```

### Get a job ETA to pickup

Before creating a Job you can ask for an estimated time of arrival at the pickup location (expressed in seconds). 
Asking for ETA is **optional** and does not prevent you from creating a job.

```c#
var job = new JobRequest
{
    Job = new Job
    {
        PickUps = new List<PickUp>
        {
            new PickUp
            {
                Address = "12 rue rivoli, 75001 Paris"
            }
        },
        DropOffs = new List<DropOff>
        {
            new DropOff
            {
                Address = "42 rue rivoli, 75001 Paris",
                PackageType = PackageSizeType.small
            }
        }
    }
};
    
var eta = await StuartApi.Job.RequestEta(job);
eta; // example: 672
```
