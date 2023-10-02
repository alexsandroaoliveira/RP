# RP

RP as a payment provider needs YOU to develop its new Authorization system and is willing to
pay accordingly!

## Table of Contents

- [Tech](#tech)
- [Modules](#moduless)
    - [Identity](#identity)
    - [Card management module](#card-management-moduler)
    - [Payment Fees module](#payment-fees-module)
    - [UFE Simulator](#ufe-simulator)
- [Development](#development)
- [Roadmap](#roadmap)

## Tech

RapidPay was developed using:
- .Net core 6 WebApi.
- XUnit framework for unity tests.
- Swagger with login.
- Jwt Autentication.
- Async methods.
- In-Memory data storage using thread-safe collections.

## Modules

### Identity

Token generator for test porpose only.

Endpoints:
- GenerateToken (`userid` input should be `test` or it will be denied. no password required)
- token expiration: 600 seconds.

### Card management module

Endpoints:
- Create card (card format is 15 digits)
- Pay (using the created card, and update balance)
- Get card balance

### Payment Fees module

The payment fees module is calculating the payment fee for each payment.
How do we know what is the payment fee???
Well, our approach of calculating the fee is a bit different - the payment fee is pretty random actually
and changes every day and hou.

### UFE Simulator

Every hour, the Universal Fees Exchange (UFE) randomly selects a decimal between 0 and 2.
The new fee price is the last fee amount multiplied by the recent random decimal.

## Development

Commitment history shows the development strategy. The project is structured close to DDD archteture. Unit tests was develop together with the main code layer by layer giving a good test coverage.

## Roadmap

- Swith the in-memory "Database" to a RMDBMS.
- Use a reliable identity service
- Pipelines for C.I.

