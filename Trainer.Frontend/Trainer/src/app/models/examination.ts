export class Examination {
  id? : string;
  patientId? : string;
  typePhysicalActive?: number;
  dateTime: Date = new Date();
  indicators?: number;
  status? : number;
}
