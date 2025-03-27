<script setup lang="ts">
/*
 * ชื่อไฟล์: Breadcrumb.vue
 * คำอธิบาย: ไฟล์นี้แสดงแถบนำทาง
 * ชื่อผู้เขียน/แก้ไข: นายพงศธร บุญญามา
 * วันที่จัดทำ/แก้ไข: 11 พฤศจิกายน 2567
 */
import { computed } from "vue";
import { useRoute, useRouter, RouteRecordNormalized } from "vue-router";

const route = useRoute();
const router = useRouter();

interface RouteMeta {
  breadcrumb?: string;
  parent?: string;
}

const findParentRoutes = (
  routeName: string | undefined,
  routes: Array<any> = []
) => {
  if (!routeName) return routes;
  const parentRoute = router
    .getRoutes()
    .find((route) => route.name === routeName);
  if (parentRoute) {
    routes.unshift({
      path: parentRoute.path,
      name: parentRoute.name,
      meta: parentRoute.meta as RouteMeta,
    });
    if (typeof parentRoute.meta.parent === "string") {
      return findParentRoutes(parentRoute.meta.parent, routes);
    }
  }
  return routes;
};

const breadcrumbs = computed(() => {
  let matchedRoutes = route.matched.map((route: RouteRecordNormalized) => ({
    path: route.path,
    name: route.name,
    meta: route.meta as RouteMeta,
  }));

  let currentRoute = matchedRoutes[matchedRoutes.length - 1];
  if (currentRoute && currentRoute.meta.parent) {
    const parentRoutes = findParentRoutes(currentRoute.meta.parent);
    matchedRoutes = [...parentRoutes, ...matchedRoutes];
  }

  // หากมาจากการแจ้งเตือน ให้แสดง 'แจ้งเตือน / รายละเอียด'
  if (route.query.fromNotification === "true") {
    return [
      { path: "/notification", label: "แจ้งเตือน" },
      { path: route.path, label: "รายละเอียด" },
    ];
  }

  return matchedRoutes.map((route) => ({
    path: route.path,
    label: route.meta.breadcrumb || route.name,
  }));
});
</script>

<template>
  <nav
    class="bg-white px-[24px] rounded-md w-full h-[30px]"
    aria-label="breadcrumb"
  >
    <ol class="list-reset flex">
      <li
        v-for="(breadcrumb, index) in breadcrumbs"
        :key="index"
        class="flex items-center text-Breadcrumb-Navigation"
      >
        <!-- ตรวจสอบหากเป็น breadcrumb สุดท้าย ไม่ต้องใส่ลิงก์ -->
        <div v-if="index !== breadcrumbs.length - 1">
          <router-link
            :to="breadcrumb.path"
            class="text-[#B6B7BA] hover:underline"
          >
            {{ breadcrumb.label }}
          </router-link>
          <span
            class="mx-2"
            :class="
              index === breadcrumbs.length - 2
                ? 'text-[#777777]'
                : 'text-[#B6B7BA]'
            "
            >/</span
          >
        </div>
        <span v-else class="text-[#777777]">{{ breadcrumb.label }}</span>
      </li>
    </ol>
  </nav>
</template>
