import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/Services/shop.service';
import { Product } from '../../shared/Models/Product';
import { ProductItemComponent } from '../../features/shop/product-item/product-item.component';
import { MatDialog } from '@angular/material/dialog';
import { FiltersDialogComponent } from './filters-dialog/filters-dialog.component';
import { MatButton } from '@angular/material/button';
import { MatCard } from '@angular/material/card';
import { MatIcon } from '@angular/material/icon';
@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [
    MatCard,
    ProductItemComponent,
    MatButton,
    MatIcon
],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit{

  private shopService = inject (ShopService)
  private dialogService = inject(MatDialog)
 
  products: Product[] =[];
  selectedBrands: string[] =[];
  selectedTypes: string[] = [];

  ngOnInit(): void {
   
    this.initializeShop();
  }

  initializeShop(){
    this.shopService.getBrands();
    this.shopService.getTypes();
    this.shopService.getProduct().subscribe({
      next: response =>{ 
        console.log(response);
        this.products = response.data
      },
      error: error => console.log(error),
      //complete: () => console.log('complete')

    })
  }

  openFiltersDialog(){

    const dialogRef = this.dialogService.open(FiltersDialogComponent, {
      minWidth:'500px',
      data: {
        selectedBrands: this.selectedBrands,
        selectedTypes: this.selectedTypes
      }
    });
    dialogRef.afterClosed().subscribe({
      next: result => {
        if (result){
          console.log(result);
          this.selectedBrands = result.selectedBrands;
          this.selectedTypes = result.selectedTypes;
          this.shopService.getProduct(this.selectedBrands,this.selectedTypes).subscribe({
            next: response => this.products = response.data,
            error: error=> console.log(error)
          })
        }
      }
    })
  }
}
