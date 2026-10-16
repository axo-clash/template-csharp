# --- Etape 1 : Build de l'application en NativeAOT (binaire statique musl) ---
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /app

# NativeAOT necessite une toolchain C (clang) pour la liaison native
RUN apk add --no-cache clang build-base

# Restaure les dependances puis compile en NativeAOT pour l'architecture courante
COPY template-csharp.csproj ./
RUN RID="linux-musl-$(uname -m | sed 's/aarch64/arm64/; s/x86_64/x64/')" \
    && echo $RID > /app.rid \
    && dotnet restore -r $RID

COPY . ./
RUN RID=$(cat /app.rid) \
    && dotnet publish -c Release -r $RID -o /app/publish

# --- Etape 2 : Image finale minimale (FROM scratch) ---
# Le binaire NativeAOT construit sur musl ne depend que du loader musl,
# copie depuis l'etape de build : aucun autre runtime ni librairie requis.
FROM scratch

# Port d'ecoute par defaut (surchargeable via la variable d'environnement PORT)
ENV PORT=9000
EXPOSE 9000

COPY --from=build /lib/ld-musl-*.so.1 /lib/
COPY --from=build /app/publish/template-csharp /template-csharp

ENTRYPOINT ["/template-csharp"]
