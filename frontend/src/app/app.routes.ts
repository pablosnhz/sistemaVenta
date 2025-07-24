import { Routes } from '@angular/router';
import { ListaRolComponent } from './pages/roles/lista-rol/lista-rol.component';
import { ListUserComponent } from './pages/users/list-user/list-user.component';

export const routes: Routes = [
  {
    path: 'pages/roles',
    component: ListaRolComponent,
  },
  {
    path: 'pages/users',
    component: ListUserComponent,
  },
];
