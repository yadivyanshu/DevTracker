import { Routes } from '@angular/router';

export const appRoutes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'dashboard', loadChildren: () => import('./features/dashboard/dashboard.routes').then(m => m.routes) },
  { path: 'auth', loadChildren: () => import('./features/auth/auth.routes').then(m => m.routes) },
  { path: '**', redirectTo: 'dashboard' },
];