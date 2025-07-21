import { NgClass } from '@angular/common';
import { Component } from '@angular/core';

import { MatIconModule } from '@angular/material/icon';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  imports: [MatIconModule, RouterOutlet, NgClass, RouterLink],
  standalone: true,
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss',
})
export class SidebarComponent {
  menuShow: string | null = null;

  enlace_menu(menu: string, event: Event) {
    event.preventDefault();
    this.menuShow = this.menuShow === menu ? null : menu;
  }

  esSubMenuAbierto(menu: string): boolean {
    return this.menuShow === menu;
  }

  cerraSubMenu() {
    this.menuShow = null;
  }
}
