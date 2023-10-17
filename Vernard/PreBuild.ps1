#!/usr/bin/env -S powershell -NoProfile -File

param(
	[string]$Config
)

$ErrorActionPreference = "Stop"

function Format-Xaml([string]$Directory, [switch]$Recursive, [switch]$Passive) {
	$Args = @()

	if ($Recursive) {
		$Args += "--recursive"
	}

	if ($Passive) {
		$Args += "--passive"
	}

	Write-Host "Formatting files in $(Resolve-Path $Directory) with Args: $($Args)"
	dotnet xstyler --directory $Directory @Args

	if ($LASTEXITCODE -ne 0) {
		throw "Some XAML files are not properly formatted"
	}
}

if ($Config -eq "Debug") {
	Format-Xaml . 
	Format-Xaml Views -Recursive
} else {
	Format-Xaml . -Passive
	Format-Xaml Views -Recursive -Passive
}