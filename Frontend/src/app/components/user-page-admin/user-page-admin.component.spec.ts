import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserPageAdminComponent } from './user-page-admin.component';

describe('UserPageAdminComponent', () => {
  let component: UserPageAdminComponent;
  let fixture: ComponentFixture<UserPageAdminComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UserPageAdminComponent]
    });
    fixture = TestBed.createComponent(UserPageAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
