$owner = "ThomasDoggen1"
$repo = "OpsCore"
$project = "8"

$issues = @(
    # Setup
    "Folder structuur aanmaken in alle 3 projecten",
    "NuGet packages installeren (EF Core, SQLite, CommunityToolkit.Mvvm)",
    "AppDbContext aanmaken en configureren",
    "Database connectie testen",

    # Core Models & Enums
    "AssetStatus enum aanmaken",
    "MaintenanceType enum aanmaken",
    "AssetType model aanmaken",
    "Department model aanmaken",
    "Employee model aanmaken",
    "Asset model aanmaken",
    "MaintenanceRecord model aanmaken",
    "ActivityLog model aanmaken",

    # Core Interfaces
    "IAssetTypeRepository aanmaken",
    "IDepartmentRepository aanmaken",
    "IEmployeeRepository aanmaken",
    "IAssetRepository aanmaken",
    "IMaintenanceRecordRepository aanmaken",
    "IActivityLogRepository aanmaken",

    # Core Services
    "MaintenanceService aanmaken",

    # Data Repositories
    "AssetTypeRepository implementeren",
    "DepartmentRepository implementeren",
    "EmployeeRepository implementeren",
    "AssetRepository implementeren",
    "MaintenanceRecordRepository implementeren",
    "ActivityLogRepository implementeren",
    "Eerste migration aanmaken en database genereren",

    # UI Navigatie
    "Sidebar navigatie opzetten",
    "NavigationHelper aanmaken",
    "Dependency Injection configureren in App.xaml.cs",

    # Assets module
    "AssetsViewModel aanmaken",
    "AssetsPage.xaml bouwen",
    "Assets laden en tonen in tabel",
    "Asset toevoegen (dialog/form)",
    "Asset bewerken",
    "Asset verwijderen",
    "Status badge met kleuren (StatusToColorConverter)",
    "Zoekfunctie assets",

    # Employees module
    "EmployeesViewModel aanmaken",
    "EmployeesPage.xaml bouwen",
    "Employees laden en tonen in tabel",
    "Employee toevoegen",
    "Employee bewerken",
    "Employee verwijderen",

    # Maintenance module
    "MaintenanceViewModel aanmaken",
    "MaintenancePage.xaml bouwen",
    "Overdue assets tonen",
    "Kleurcodering rijen (rood/oranje/groen)",
    "Filter tabs (Overdue / Due Soon / Ok)",
    "Onderhoud registreren",

    # Dashboard
    "DashboardViewModel aanmaken",
    "DashboardPage.xaml bouwen",
    "KPI cards (Total, Active, Due, Overdue)",
    "Asset status donut chart",
    "Assets per type bar chart",
    "Recent activity lijst",

    # Afronden
    "Seed data toevoegen (testdata)",
    "App testen op alle schermen",
    "Bug fixes"
)

foreach ($issue in $issues) {
    Write-Host "Creating issue: $issue"
    $issueUrl = gh issue create --repo "$owner/$repo" --title "$issue" --body "."
    gh project item-add $project --owner $owner --url $issueUrl
}

Write-Host "Backlog aangemaakt!"