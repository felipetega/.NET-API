import { Routes } from '@angular/router';
import { CityCardComponent } from './components/city-card/city-card.component';
import { CityCreateComponent } from './components/city-create/city-create.component';
import { CityUpdateComponent } from './components/city-update/city-update.component';
import { CityDeleteComponent } from './components/city-delete/city-delete.component';

export const routes: Routes = [
    { path: 'cities', component: CityCardComponent },
    // { path: 'create', component: CityCreateComponent },
    // { path: 'update', component: CityUpdateComponent },
    // { path: 'delete', component: CityDeleteComponent },
];
