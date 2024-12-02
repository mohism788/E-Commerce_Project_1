import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../../core/Services/shop.service';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../../../shared/Models/Product';
import { CurrencyPipe } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatDivider } from '@angular/material/divider';

@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [
    CurrencyPipe,
    MatButton,
    MatIcon,
    MatFormField,
    MatInput,
    MatLabel,
    MatDivider
  ],
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.scss'
})
export class ProductDetailComponent implements OnInit {

  private shopService = inject(ShopService);
  private activatedRoute = inject(ActivatedRoute);
  product?: Product;

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct(){
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if(!id) return;

    this.shopService.getProductById(+id).subscribe({
        next: product => this.product = product,
        error: error => console.log(error)

    })
  }
}
