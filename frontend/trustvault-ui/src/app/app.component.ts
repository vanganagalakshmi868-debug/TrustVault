import { Component } from '@angular/core';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['../styles/style.css']
})
export class AppComponent {
  title = 'TrustVault';
  API_URL = "http://localhost:5000/api";
  async loginUser(email: string, password: string) {
    const res = await fetch(`${this.API_URL}/Auth/login`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ email, password })
    });
    return await res.json();
  }
  saveToken(token: string) {
    localStorage.setItem("token", token);
  }
  logout() {
    localStorage.removeItem("token");
    window.location.href = "/login";
  }
}