import {Component, OnInit} from "@angular/core";
import {Patient} from "../../../models/patient";
import {PatientService} from "../../../services/patient.service";

@Component({
  selector: 'app-getPatients',
  templateUrl: './getPatients.component.html'
})

export class GetPatientsComponent implements OnInit {
  patients: Patient[] = [];
  patientToEdit?: Patient;

  constructor(private superHeroService: PatientService) {}

  ngOnInit(): void {
    this.superHeroService
      .getPatients()
      .subscribe((result: Patient[]) => (this.patients = result));
  }

  // updateHeroList(heroes: SuperHero[]) {
  //   this.heroes = heroes;
  // }
  //
  // initNewHero() {
  //   this.heroToEdit = new SuperHero();
  // }
  //
  // editHero(hero: SuperHero) {
  //   this.heroToEdit = hero;
  // }
}
