# Org Company Management

Angular 16+ frontend for a business CRUD system (Organizations and Companies) using Angular Material and feature-based architecture.

## Requirements

- Node.js 16+ and npm

## Setup

```bash
npm install
npm start
```

Open http://localhost:4200

## Build

```bash
npm run build
```

## Project structure

```
src/app
├── core/                 # Core module (optional cross-cutting concerns)
├── shared/               # Shared module
│   ├── components/       # confirmation-dialog, reusable-table, autocomplete-input
│   └── services/        # snackbar.service
├── organization/        # Organization feature (lazy-loaded)
│   ├── models/
│   ├── services/
│   └── pages/           # add-organization, organization-search
├── company/              # Company feature (lazy-loaded)
│   ├── models/
│   ├── services/
│   └── pages/           # add-company, company-search
├── app.module.ts
└── app-routing.module.ts
```

## Features

- **Organizations**: Add organization (reactive form, Country autocomplete), search with filters (name, code, Country), table with pagination/sort, edit/delete actions.
- **Companies**: Add company (organization dropdown, "Get Organization Details" with override confirmation), search with filters, table with pagination/sort, edit/delete actions.
- **Shared**: Confirmation dialog, reusable data table, snackbar service, autocomplete input component.
