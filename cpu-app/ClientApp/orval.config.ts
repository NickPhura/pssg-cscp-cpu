import { defineConfig } from "orval";

export default defineConfig({
  "cpu-api": {
    input: {
      target: "http://localhost:5000/swagger/v1/swagger.json",
    },
    output: {
      mode: "tags-split",
      target: "./src/app/core/api/services",
      schemas: "./src/app/core/api/models",
      client: "angular",
      httpClient: "angular",
      mock: false,
      clean: true,
      prettier: true,
      tsconfig: "./tsconfig.json",
    },
    hooks: {
      afterAllFilesWrite: "prettier --write",
    },
  },
});
