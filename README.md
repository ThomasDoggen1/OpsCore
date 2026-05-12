# OpsCore

## Wireframe
https://www.figma.com/make/6dw9sDmrJTz2YzNkly0sZn/OpsCore-IT-Asset-Management-App?t=gKgfJgBK3Ilg3Ilj-20&fullscreen=1&code-node-id=0-9

## Root structure
OpsCore/
├── OpsCore.Core/
│   ├── Models/
│   │   ├── Asset.cs
│   │   ├── Employee.cs
│   │   ├── Department.cs
│   │   ├── AssetType.cs
│   │   ├── MaintenanceRecord.cs
│   │   └── ActivityLog.cs
│   ├── Interfaces/
│   │   ├── IAssetRepository.cs
│   │   ├── IEmployeeRepository.cs
│   │   ├── IDepartmentRepository.cs
│   │   ├── IAssetTypeRepository.cs
│   │   ├── IMaintenanceRecordRepository.cs
│   │   └── IActivityLogRepository.cs
│   ├── Enums/
│   │   ├── AssetStatus.cs
│   │   └── MaintenanceType.cs
│   └── Services/
│       └── MaintenanceService.cs
│
├── OpsCore.Data/
│   ├── Context/
│   │   └── AppDbContext.cs
│   ├── Repositories/
│   │   ├── AssetRepository.cs
│   │   ├── EmployeeRepository.cs
│   │   ├── DepartmentRepository.cs
│   │   ├── AssetTypeRepository.cs
│   │   ├── MaintenanceRecordRepository.cs
│   │   └── ActivityLogRepository.cs
│   └── Migrations/
│       └── (automatisch gegenereerd)
│
└── OpsCore.UI/
    ├── Views/
    │   ├── DashboardPage.xaml
    │   ├── AssetsPage.xaml
    │   ├── EmployeesPage.xaml
    │   └── MaintenancePage.xaml
    ├── ViewModels/
    │   ├── DashboardViewModel.cs
    │   ├── AssetsViewModel.cs
    │   ├── EmployeesViewModel.cs
    │   └── MaintenanceViewModel.cs
    ├── Converters/
    │   └── StatusToColorConverter.cs
    └── Helpers/
        └── NavigationHelper.cs

    
