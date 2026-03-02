git clone https://github.com/CIRFMF/ksef-pdf-generator
cd ksef-pdf-generator
npm install
npm run build -- --minify false
copy dist\ksef-fe-invoice-converter.umd.cjs ..\
cd ..
rd /q /s ksef-pdf-generator
git apply profak-mod.diff
