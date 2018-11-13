<template>
  <div class="flex-center">
    <div class="container">
      <div v-if="this.loading">
        <span>Loading...</span>
      </div>
      <div v-else>
        <form class="form">
            <input placeholder="Search" id="name-search" type="text" v-model="nameFilter"/>
        </form>
        <div v-if="!Array.isArray(products) || !products.length">
          <br/>
          <span>No Products Found</span>
        </div>
        <table v-else class="table">
          <thead>
            <th class="product-table__header" @click="orderBy('Name')">Name<span v-if="ordering.type === 'Name'" v-html="displayOrderArrow"></span></th>
            <th class="product-table__header" @click="orderBy('Cost')">Price<span v-if="ordering.type === 'Cost'" v-html="displayOrderArrow"></span></th>
          </thead>
          <tr v-for="product in products" :key="product.id">
            <td>{{product.Name}}</td>
            <td>£{{product.Cost/100}}</td>
          </tr>
        </table>
      </div>
    </div>
  </div>
</template>

<script>
import { sortByName, sortByCost } from "./helpers/sorting.js";
export default {
  data: function() {
    return {
      loading: true,
      nameFilter: "",
      products: [],
      ordering: {
        type: "Default",
        orderReverse: false
      }
    };
  },
  watch: {
    nameFilter: function(newName, oldName) {
      this.getProducts(`/api/products?name=${newName}`);
    }
  },
  created: function() {
    this.getProducts("/api/products").then(
      function() {
        this.loading = false;
      }.bind(this)
    );
  },
  computed: {
    displayOrderArrow: function() {
      if (this.ordering.orderReverse) {
        return "&#8593;";
      } else {
        return "&#8595;";
      }
    }
  },
  methods: {
    getProducts: function(url) {
      return fetch(url)
        .then(function(response) {
          return response.json();
        })
        .then(
          function(json) {
            this.products = this.order(json);
          }.bind(this)
        );
    },
    orderBy: function(type) {
      if (this.ordering.type === type) {
        this.ordering.orderReverse = !this.ordering.orderReverse;
      } else {
        this.ordering.orderReverse = false;
      }
      this.ordering.type = type;

      this.products = this.order(this.products);
    },
    order: function(list) {
      var type = this.ordering.type;
      var reverse = this.ordering.orderReverse;
      var products = list;
      switch (type) {
        case "Name":
          products = sortByName(list, reverse);
          break;

        case "Cost":
          products = sortByCost(list, reverse);
          break;

        default:
          break;
      }

      return products;
    }
  }
};
</script>

<style>
.product-table__header {
  cursor: pointer;
}
.product-table__header:hover {
  background-color: #e4e4e4;
}
</style>