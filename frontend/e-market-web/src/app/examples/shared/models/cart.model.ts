import { ProductPicture } from "../../admin/models/product.model"

export interface CartItemAdd {
    publicId: string,
    quantity: number

}

export interface CartItem {
    name: string,
    price: number,
    brand: string,
    picture: ProductPicture
    quantity: number,
    publicId: string
}

export interface Cart {
    products: CartItem[],
}

export interface StripeSession {
    id: string;
}