import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CityCardComponent } from './city-card.component';

describe('CityCardComponent', () => {
  let component: CityCardComponent;
  let fixture: ComponentFixture<CityCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CityCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CityCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should track items by id', () => {
    component.data = [
      { id: 1, cityName: 'City A', stateName: 'State A' },
      { id: 2, cityName: 'City B', stateName: 'State B' },
    ];

    fixture.detectChanges();

    component.data = [
      { id: 1, cityName: 'Updated City A', stateName: 'Updated State A' },
      { id: 2, cityName: 'City B', stateName: 'State B' },
    ];

    fixture.detectChanges();

    const compiled = fixture.debugElement.nativeElement;

    expect(compiled.querySelector('.city-name').textContent).toContain('Updated City A');
    expect(compiled.querySelector('.state-name').textContent).toContain('Updated State A');
  });
});
